export default class ExpressDeliveryWidget extends HTMLElement {
    constructor() {
        super();
    }

    connectedCallback() {
        this.timeoutHandle = setInterval(() => {
            const target = this.parentElement;
            const url = target.dataset.dwCurrentlyLoaded;
            if (url)
                Actions.NavigateUrl(target.dataset.dwCurrentlyLoaded, target, true, true);
        }, 1000)
    }

    disconnectedCallback() {
        clearInterval(this.timeoutHandle);
    }
}

customElements.define("express-delivery-widget", ExpressDeliveryWidget);