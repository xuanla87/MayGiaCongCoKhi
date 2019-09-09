var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/";
        });
        $('#btnUpdatecart').off('click').on('click', function () {
            var listP = $('.txtquantity');
            var cartlist = [];
            $.each(listP, function (i, item) {
                cartlist.push({
                    Quantity: $(item).val(),
                    Product: {
                        Id: $(item).data('id')
                    }
                });
            });

            $.ajax({
                url: 'Cart/Update',
                data: { CartItemModel: JSON.stringify(cartlist) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = '/gio-hang';
                    }
                }
            })
        });
        $('#btnPay').off('click').on('click', function () {
            window.location.href = '/thanh-toan';
        });
        $('#btnDeletecartAll').off('click').on('click', function () {
            $.ajax({
                url: 'Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = '/gio-hang';
                    }
                }
            })
        });
        $('.btnDelete').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                url: 'Cart/Delete',
                data: { Id: $(this).data('id') },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = '/gio-hang';
                    }
                }
            })
        });
    }
}
cart.init();