/// <reference path="jquery-3.1.1.js"/>

$("#txtara").keyup(function () {
    var ara = this.value;
    console.log(ara);
    $.ajax({
        url: "../Urun/Bul?key=" + ara, method: "GET"
    }).done(function (response) {
        console.log(response);
        $("#lblmesaj").fadeIn(1000);
        
        if (response.length === 0) {
            $("#lblmesaj").html("Hiçbir sonuç bulunamadı");
            $("#lblmesaj").attr("class", "label label-danger");
            $("#tblsonuc").fadeOut(1000);
            
        }
        else {
            $("#lblmesaj").html(response.length + " Adet sonuç bulundu");
            $("#lblmesaj").attr("class", "label label-success");
            $("#tblsonuc").fadeIn(1000);
            $("#uruntablebody").empty();
            $.each(response,
               function (key, value) {
                   urunleriekranabas(key, value);
               });
        }
    });
})
function urunleriekranabas(key, value) {
   var tr= document.createElement("tr");
   var th = document.createElement("th");
   $(th).attr("scope", "row");
   $(th).html(key + 1);
   var tdurunadi = document.createElement("td");
   $(tdurunadi).html(value.ProductName);
   var tdfiyat = document.createElement("td");
   $(tdfiyat).html(value.UnitPrice);
   var tdstok = document.createElement("td");
   $(tdstok).html(value.UnitsInStock);
   var tdsatis = document.createElement("td");
   if (value.Discontinued) {
       $(tdsatis).html("<span class='text-danger'>Satışta değil</span>");
   } else {
       $(tdsatis).html("<span class='text-succes'>Satışta</span>");
   }
   var tdduzenle = document.createElement("td");
   $(tdduzenle).html("<a href='../Urun/Duzenle/" + value.ProductsID+ "' class='btn btn-block btn-warning btn-block'>Düzenle</a>");
   $(tr).append(th).append(tdurunadi).append(tdfiyat).append(tdstok).append(tdsatis).append(tdduzenle).appendTo($("#uruntablebody"));


}

