function Pay(pharmacy) {
    var productId = @Model.Id;
    var currentQuantity = document.getElementById('quantity-1').value;


    fetch('@Url.Action("Payment", "Reservation")', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            location: pharmacy.Location,
            productId: productId,
            quantity: currentQuantity
        })
    })
        .then(response => {
            if (response.redirected) {
                window.location.href = response.url;
            }
        })
        .catch(error => console.error('Error:', error));
}