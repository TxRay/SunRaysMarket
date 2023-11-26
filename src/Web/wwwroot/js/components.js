function initializeModal(outletId) {
    const modal = document.getElementById('modal');
    const modalContent = document.getElementById('modalContent');
    const modalCloseBtn = document.getElementById('modalCloseBtn');

    function closeModal() {
        const modalOutlet = document.getElementById(outletId);
        modalOutlet.innerHTML = '';
    }

    modalCloseBtn.addEventListener('click', closeModal);

    modal.addEventListener('click', (e) => {
        if (!modalContent.contains(e.target)) {
            closeModal();
        }
    });
}

function initializeCartControls(idSuffix) {
    const timeout = 800;
    
    function renderSuffixString(idSuffix) {
        return idSuffix ? `--${idSuffix}` : '';
    }
    
    function onQuantityUpdated(quantity) {
        console.log(quantity);
    }
    
    const addToCartBtn = document.getElementById(`addToCartBtn${renderSuffixString(idSuffix)}`);
    const cartControls = document.getElementById(`cartControls${renderSuffixString(idSuffix)}`);
    const cartControlsRemoveBtn = document.getElementById(`cartControlsRemoveBtn${renderSuffixString(idSuffix)}`);
    const quantityControlsInput = document.getElementById(`quantityControlsInput${renderSuffixString(idSuffix)}`);
    const quantityControlsIncrementBtn = document.getElementById(`quantityControlsIncrementBtn${renderSuffixString(idSuffix)}`);
    const quantityControlsDecrementBtn = document.getElementById(`quantityControlsDecrementBtn${renderSuffixString(idSuffix)}`);


    addToCartBtn.addEventListener('click', () => {
        addToCartBtn.classList.add('product-details__btn--hidden');
        cartControls.classList.add('cart-controls--visible');
        quantityControlsInput.value = 1;
    });

    quantityControlsDecrementBtn.addEventListener('click', () => {

        
        if (parseInt(quantityControlsInput.value) > 1) {
            quantityControlsInput.value = parseInt(quantityControlsInput.value) - 1;

            if (this.decrementTimoutId) {
                clearTimeout(this.decrementTimoutId);
            }
            
            this.decrementTimoutId = setTimeout(() => onQuantityUpdated(quantityControlsInput.value), timeout);
        }
    });


    quantityControlsIncrementBtn.addEventListener('click', () => {
        quantityControlsInput.value = parseInt(quantityControlsInput.value) + 1;
        
    if (this.incrementTimoutId) {
        clearTimeout(this.incrementTimoutId);
    }
    
    this.incrementTimoutId = setTimeout(() => onQuantityUpdated(quantityControlsInput.value), timeout);
    });
    
    quantityControlsInput.addEventListener('change', () => {
        if (parseInt(quantityControlsInput.value) < 1) {
            quantityControlsInput.value = 1;
        }
        
        onQuantityUpdated(quantityControlsInput.value);
    });
    
    
    cartControlsRemoveBtn.addEventListener('click', () => {
        addToCartBtn.classList.remove('product-details__btn--hidden');
        cartControls.classList.remove('cart-controls--visible');
        quantityControlsInput.value = 1;
    });

}