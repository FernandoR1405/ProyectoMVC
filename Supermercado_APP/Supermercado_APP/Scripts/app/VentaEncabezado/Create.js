
//listado de detalle
let ListadoDetalle = [];

//variables de articulo
var prod_Id = 0;
var prod_Precio = 0;
var prod_Descripcion = "";
var prod_Cantidad = 0;

//total de venta
var VentEnc_Total = 0;


//cargar el listado de productos 
$(document).ready(function () {

    //vaciar el dropdown de productos 
    $("#DDLProductos").empty();

    console.log("Obtener artículos");

    $.ajax({
        url: "/VentaEncabezado/ObtenerListadoProductos",
        method: "GET",
        dataType: 'json'
    }).done(function (data) {

        console.log(data);

        //llenar el primer registro del ddl
        var Options = "<option val='0'>Seleccione un producto...</option>";
        
        //dibujar los option con registros de productos
        data.forEach(function (item, index) {
            Options += "<option value='" + item.Prod_Id + "' data-id='" + item.Prod_Id + "' data-precio='" + item.Prod_Precio + "'>" + item.Prod_Descripcion + " - L. " + item.Prod_Precio + "</option>";
        });

        //agregar la opción al dropdown
        $("#DDLProductos").html(Options);

    }).fail(function () {
        console.log("Ha ocurrido un error.")
    });
});

//detectar los cambios en el ddl de productos
$("#DDLProductos").change(function () {
    //actualizar la descripcion del producto seleccionado
    prod_Descripcion = $(this).children("option:selected").text();

    var OptionSelected = $(this).children("option:selected");
    
    prod_Id = OptionSelected.data('id');
    prod_Precio = OptionSelected.data('precio');

    console.log(prod_Id)
    console.log(prod_Precio)
});

//agregar linea al detalle de la factura
$("#btnAgregarDetalle").click(function () {
    //cambiar el valor de la variable cantidad
    prod_Cantidad = $("#txtCantidad").val();

    //agregar detalle de venta a la lista
    ListadoDetalle.push({ prod_Id: prod_Id, prod_Precio: prod_Precio, prod_Cantidad: prod_Cantidad });

    //ListadoDetalle.push(DetalleVenta);
    console.log("AgregarDetalle");
    console.log(ListadoDetalle);

    //llamar funcion que inserta detalle en la tabla
    InsertarDetalleFactura();
});

//llamar el action result de insert
$("#btnCreate").click(function () {
    var VentEnc_Id = $("#VentEnc_Total").val();

    var RespuestaValidacion = GuardarListaDetalleSesion();

    if (RespuestaValidacion == true) {

        let DataForm = [
            { name: "NoFactura", value: 1 },
            { name: "Total", value: parseFloat(VentEnc_Id) }
        ];
        console.log(DataForm);

        //hacer llamado al controlador
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/VentaEncabezado/CreateFactura",
            data: DataForm
        }).done(function (data) {

            console.log(data);

            if (data == "Error") {
                console.log("Bien");
            }
            else {
                respuesta = false;
                console.log("Ha ocurrido un error.")
            }
        }).fail(function () {
            respuesta = false;
            console.log("Ha ocurrido un error.")
        });

    }
});


//metodo para guardar la lista del detalle en una sesión
function GuardarListaDetalleSesion() {
    //variable para validar respuesta
    var respuesta = true;

    //convertir a json la lista del detalle
    var dataForm1 = JSON.stringify(ListadoDetalle);

    //hacer llamado al controlador
    $.ajax({
        type: "POST",
        url: "/VentaEncabezado/ListaDetalleSesion",
        contentType: "application/json; charset=utf-8",
        dataType: "JSON",
        data: dataForm1,
        traditional: true
    }).done(function (data) {

        console.log(data);

        if (data == "Bien") {
            console.log("Bien");
        }
        else {
            respuesta = false;
            console.log("Ha ocurrido un error.")
        }
    }).fail(function () {
        respuesta = false;
        console.log("Ha ocurrido un error.")
    });

    return respuesta;
}

//función para agregar fila a la tabla de detalle
function InsertarDetalleFactura() {

    //actualizar la variable de total
    VentEnc_Total += prod_Cantidad * prod_Precio;
    //actualizar el input de total
    $("#VentEnc_Total").val(VentEnc_Total);
    //agregar la fila a la tabla
    var row = "<tr><td>" + prod_Id + "</td> <td>" + prod_Descripcion + "</td> <td>" + prod_Precio + "</td> <td>" + prod_Cantidad + "</td> <td>" + prod_Cantidad * prod_Precio + "</td> </tr>";
    $("#tblDetalleProducto").append(row);
}
