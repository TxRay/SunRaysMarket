function initializeCartControls(idSuffix) {
    const cartControls = document.getElementById(`cartControls-${idSuffix}`);
    const addToCartBtn = document.getElementById(`addToCartBtn-${idSuffix}`);
    const quantityControls = document.getElementById(`quantityControls-${idSuffix}`);
    const removeFromCartBtn = document.getElementById(`removeFromCartBtn-${idSuffix}`);
    const cartControlQuantity = document.getElementById(`cartControlQuantity-${idSuffix}`);
    const decrementBtn = document.getElementById(`decrementQuantityBtn-${idSuffix}`);
    const incrementBtn = document.getElementById(`incrementQuantityBtn-${idSuffix}`);

    const productId = parseInt(cartControls.dataset.productId);
    let cartId = parseInt(cartControls.dataset.cartId);
    let cartItemId = parseInt(cartControls.dataset.cartItemId);
    console.log(`CartItemId: ${cartItemId}`);
    let quantity = parseInt(cartControlQuantity.value);

    async function checkIfCartExists() {
        const exists = await fetch(`/api/cart/exists`, {
            method: 'GET',
            headers: {
                accept: 'application/json'
            }
        })
            .then(response => response.json())
            .then(data => data.cartExists)
            .catch(error => console.error(error));

        console.log(`Cart exists: ${exists}`);

        return exists;
    }

    async function createCart() {
        const cartId = await fetch(`/api/cart/create`,
            {
                method: 'POST',
                headers: {
                    accept: 'application/json'
                },
            })
            .then(response => response.json())
            .then(data => data.cartId)
            .catch(error => console.error(error));

        console.log(`Cart created: ${cartId}`);

        return cartId;
    }

    async function addItemToCart(initialQuantity = 1) {
        const itemId = await fetch(`/api/cart/add-item`,
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    accept: 'application/json'
                },
                body: JSON.stringify({
                    ProductId: productId,
                    Quantity: initialQuantity
                })
            })
            .then(response => response.json())
            .then(data => data.itemId)
            .catch(error => console.error(error));

        console.log(`Item added to cart: ${itemId}`);

        return itemId;
    }

    async function removeItemFromCart() {
        await fetch(`/api/cart/remove-item`,
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    accept: 'application/json'
                },
                body: JSON.stringify({
                    ItemId: cartItemId
                })
            })
            .catch(error => console.error(error));
    }

    async function updateItemQuantity(newQuantity) {
        const updatedQuantity = await fetch(`/api/cart/update-item-quantity`,
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    accept: 'application/json'
                },
                body: JSON.stringify({
                    CartItemId: cartItemId,
                    OldQuantity: quantity,
                    NewQuantity: newQuantity
                })
            })
            .then(response => response.json())
            .then(data => data.updatedQuantity)
            .catch(error => console.error(error));

        console.log(`Item quantity updated: ${updatedQuantity}`);

        return updatedQuantity;
    }

    addToCartBtn.addEventListener('click', async () => {
        const cartExists = await checkIfCartExists();
        console.log(`Cart exists from addToCartBtn: ${cartExists}`);
        if (!cartExists) {
            cartId = await createCart();
        }
        cartControlQuantity.value = quantity = 1;
        cartItemId = await addItemToCart(quantity)
        addToCartBtn.classList.add('product-details__btn--hidden');
        quantityControls.classList.add('cart-controls--visible');
    });

    removeFromCartBtn.addEventListener('click', async () => {
        await removeItemFromCart();
        addToCartBtn.classList.remove('product-details__btn--hidden');
        quantityControls.classList.remove('cart-controls--visible');
    });

    decrementBtn.addEventListener('click', async () => {
        if (quantity > 1) {
            cartControlQuantity.value = quantity = await updateItemQuantity(quantity - 1);
        }
    });

    incrementBtn.addEventListener('click', async () => {
        cartControlQuantity.value = quantity = await updateItemQuantity(quantity + 1);
    });

}