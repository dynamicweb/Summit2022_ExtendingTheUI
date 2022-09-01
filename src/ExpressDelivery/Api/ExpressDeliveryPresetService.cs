using Dynamicweb.Data;
using System.Data;

namespace ExpressDelivery.Api
{
    internal static class ExpressDeliveryPresetService
    {
        public static ExpressDeliveryPreset? GetExpressDeliveryById(long id)
            => GetEntity(CommandBuilder.Create("SELECT * FROM [ExpressDeliveryPreset] WHERE [ExpressDeliveryPresetId] = {0}", id));

        public static ExpressDeliveryPreset? GetExpressDeliveryPresetByOrderId(string orderId)
            => GetEntity(CommandBuilder.Create(@"
                SELECT * FROM [ExpressDeliveryPreset] INNER JOIN [ExpressDeliveryPresetRelation]
                    ON [ExpressDeliveryPresetId] = [ExpressDeliveryPresetRelationExpressDeliveryPresetId]
                WHERE [ExpressDeliveryPresetRelationOrderId] = {0}", orderId));

        public static IEnumerable<ExpressDeliveryPreset> GetExpressDeliveries()
            => GetEntities(CommandBuilder.Create("SELECT * FROM [ExpressDeliveryPreset]"));

        public static bool Save(ExpressDeliveryPreset preset)
        {
            var sql = new CommandBuilder();

            sql.Add("MERGE ExpressDeliveryPreset WITH (SERIALIZABLE) AS T");
            sql.Add("USING (VALUES ");
            sql.Add("   ({0},{1},{2},{3},{4},{5})", preset.Id, preset.Name, preset.Hours, preset.OverHalfWayText, preset.UnderHalfWayText, preset.TooLateText);
            sql.Add(") AS S (ExpressDeliveryPresetId, ExpressDeliveryPresetName, ExpressDeliveryPresetHours, ExpressDeliveryPresetUnderHalfWayText, ExpressDeliveryPresetOverHalfWayText, ExpressDeliveryPresetTooLateText)");
            sql.Add("   ON S.ExpressDeliveryPresetId = T.ExpressDeliveryPresetId");
            sql.Add("WHEN MATCHED THEN");
            sql.Add("   UPDATE SET T.ExpressDeliveryPresetName = S.ExpressDeliveryPresetName, T.ExpressDeliveryPresetHours = S.ExpressDeliveryPresetHours, T.ExpressDeliveryPresetUnderHalfWayText = S.ExpressDeliveryPresetUnderHalfWayText, T.ExpressDeliveryPresetOverHalfWayText = S.ExpressDeliveryPresetOverHalfWayText, T.ExpressDeliveryPresetTooLateText = S.ExpressDeliveryPresetTooLateText");
            sql.Add("WHEN NOT MATCHED THEN");
            sql.Add("   INSERT (ExpressDeliveryPresetName, ExpressDeliveryPresetHours, ExpressDeliveryPresetUnderHalfWayText, ExpressDeliveryPresetOverHalfWayText, ExpressDeliveryPresetTooLateText) VALUES (S.ExpressDeliveryPresetName, S.ExpressDeliveryPresetHours, S.ExpressDeliveryPresetUnderHalfWayText, S.ExpressDeliveryPresetOverHalfWayText, S.ExpressDeliveryPresetTooLateText)");
            sql.Add("OUTPUT INSERTED.ExpressDeliveryPresetId;");

            long identity = 0;
            try
            {
                identity = (long)Database.ExecuteScalar(sql);
                if (identity != preset.Id)
                    preset.Id = identity;
            }
            catch { }

            return identity > 0;
        }

        public static bool Delete(long presetId)
        {
            try
            {
                return Database.ExecuteNonQuery(CommandBuilder.Create("DELETE FROM ExpressDeliveryPreset WHERE ExpressDeliveryPresetId = {0}", presetId)) > 0;
            }
            catch
            {
                return false;
            }
        }

        public static bool AttachOrUpdatePreset(string orderId, long presetId)
        {
            CommandBuilder sql = new();

            sql.Add("MERGE ExpressDeliveryPresetRelation WITH (SERIALIZABLE) AS T");
            sql.Add("USING (VALUE ");
            sql.Add("   ({0},{1})", orderId, presetId);
            sql.Add(") AS S (ExpressDeliveryPresetRelationOrderId, ExpressDeliveryPresetRelationExpressDeliveryPresetId)");
            sql.Add("   ON S.ExpressDeliveryPresetRelationOrderId = T.ExpressDeliveryPresetRelationOrderId");
            sql.Add("WHEN MATCHED THEN");
            sql.Add("   UPDATE SET T.ExpressDeliveryPresetRelationExpressDeliveryPresetId = S.ExpressDeliveryPresetRelationExpressDeliveryPresetId");
            sql.Add("WHEN NOT MATCHED THEN");
            sql.Add("   INSERT (ExpressDeliveryPresetRelationOrderId, ExpressDeliveryPresetRelationExpressDeliveryPresetId) VALUES (S.ExpressDeliveryPresetRelationOrderId, S.ExpressDeliveryPresetRelationExpressDeliveryPresetId);");

            int affectedRows = 0;
            try
            {
                affectedRows = Database.ExecuteNonQuery(sql);
            }
            catch { }

            return affectedRows > 0;
        }

        public static bool DetachPreset(string orderId)
        {
            try
            {
                return Database.ExecuteNonQuery(CommandBuilder.Create("DELETE FROM ExpressDeliveryPresetRelation WHERE ExpressDeliveryPresetRelationOrderId = {0}", orderId)) > 0;
            }
            catch
            {
                return false;
            }
        }

        private static ExpressDeliveryPreset? GetEntity(CommandBuilder cb)
        {
            var reader = Database.CreateDataReader(cb);
            if (reader.Read())
                return MapData(reader);
            return null;
        }

        private static IEnumerable<ExpressDeliveryPreset> GetEntities(CommandBuilder cb)
        {
            var reader = Database.CreateDataReader(cb);
            while (reader.Read())
                yield return MapData(reader);
        }

        private static ExpressDeliveryPreset MapData(IDataReader reader) => new()
        {
            Id = reader.GetInt64(reader.GetOrdinal("ExpressDeliveryPresetId")),
            Name = reader.GetString(reader.GetOrdinal("ExpressDeliveryPresetName")),
            Hours = reader.GetInt32(reader.GetOrdinal("ExpressDeliveryPresetHours")),
            UnderHalfWayText = reader.GetString(reader.GetOrdinal("ExpressDeliveryPresetUnderHalfWayText")),
            OverHalfWayText = reader.GetString(reader.GetOrdinal("ExpressDeliveryPresetOverHalfWayText")),
            TooLateText = reader.GetString(reader.GetOrdinal("ExpressDeliveryPresetTooLateText")),
        };
    }
}
