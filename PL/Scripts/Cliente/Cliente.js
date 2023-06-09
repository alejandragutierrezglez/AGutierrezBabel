/// <reference path="Cliente.js" />

$(document).ready(function () { //click
    GetAll();
});

function GetAll() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:55535/api/Cliente/GetAll',
        success: function (result) { //200 OK 
            $('#tblClientes tbody').empty();
            $.each(result.objects, function (i, cliente) {
                var filas =
                    '<tr>'

                    + '<td class="text-center"> '
                    + '<a href="#" onclick="GetById(' + cliente.idCliente + ')">'
                    + '<i class="bi bi-pencil-fill" style="color: #f57809"></i>'
                    + '</a> '
                    + '</td>'
                    + "<td class='text-center'>" + cliente.idCliente + "</td>"
                    + '<td class="text-center"> <button class="btn btn-danger" onclick="Delete(' + cliente.idCliente + ')"><i class="bi bi-eraser-fill" style="color: #FFFFFF"></i></span></button></td>'

                    + "</tr>";
                $("#tblClientes tbody").append(filas);
            });
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });
};
