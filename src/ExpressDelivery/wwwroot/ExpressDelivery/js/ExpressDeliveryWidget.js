export default class ExpressDeliveryWidget extends HTMLElement {
    constructor() {
        super();
    }

    connectedCallback() {
        this.timeoutHandle = setTimeout(() => {
            Actions.navigateUrl(this.dataset.dwCurrentlyLoaded, this, true);
        }, 1000)
    }

    disconnectedCallback() {
        clearTimeout(this.timeoutHandle);
    }
}

customElements.define("express-delivery-widget", ExpressDeliveryWidget);