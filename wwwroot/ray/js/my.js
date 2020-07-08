
function hesabchange() {
    if ($("#txtmoinB").val() != "") {
        $("#txtpersonB").prop('disabled', true);
        $("#hfpersonidB").val("");
    }
    else
        $("#txtpersonB").prop('disabled', false);
    if ($("#txtpersonB").val() != "") {
        $("#txtmoinB").prop('disabled', true);
        $("#hfmoinidB").val("");
        $("#hftafidB").val("");
    }
    else
        $("#txtmoinB").prop('disabled', false);



    if ($("#txtmoinS").val() != "") {
        $("#txtpersonS").prop('disabled', true);
        $("#hfpersonidS").val("");
    }
    else
        $("#txtpersonS").prop('disabled', false);
    if ($("#txtpersonS").val() != "") {
        $("#txtmoinS").prop('disabled', true);
        $("#hfmoinidS").val("");
        $("#hftafidS").val("");
    }
    else
        $("#txtmoinS").prop('disabled', false);
}


//import { debug } from "util";
function searchlist(c) {
    var value = c;
    if (c == '0') {
        var value = "";
        $("#myTable tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    }
    else {
        $("#myTable tbody tr").filter(function () {
            if (value == 'ا')
                if ($.trim($(this).text().toLowerCase()).charAt(0) == 'آ')
                    value = 'آ'
            $(this).toggle($.trim($(this).text().toLowerCase()).charAt(0) == value)
        });
    }
}

function validatecoding() {
    if ($("#txtmoin").val() === "")
        $("#hfmoinid").val("");
    if ($("#txttaf").val() === "")
        $("#hftafid").val("");
    if ($("#txtperson").val() === "")
        $("#hfpersonid").val("")
    return true;
}

$("body").on("keyup", "input.TextBoxn", function (event) {

    // skip for arrow keys
    if (event.which >= 37 && event.which <= 40) return;

    // format number
    $(this).val(function (index, value) {
        return value
            .replace(/\D/g, "")
            .replace(/\B(?=(\d{3})+(?!\d))/g, ",")
            ;
    });
});
function loadperson() {
    $.ajax({
        type: "POST",
        url: "/Main/getperson",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#txtperson,#txtpersonB,#txtpersonBE,#txtpersonE , #txtpersonkharj , #txtbazar').autocomplete({
                source: data,
                select: function (event, ui) {
                    $("#txtperson,#txtpersonB,#txtpersonBE,#txtpersonE , #txtpersonkharj ").html(ui.item["namekamel"]);
                    $("#txtbazar").val(ui.item["namekamel"]);
                    $("#hfpersonid,#hfpersonidB,#hfpersonidBE,#hfpersonidE , #hfpersonkharj , #hfbazar").val(ui.item["id"]);
                }
            });
        }
    });

}



function loadpersonbazar() {
    $.ajax({
        type: "POST",
        url: "/Main/getperson",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#txtbazar').autocomplete({
                source: data,
                select: function (event, ui) {
                    $("#txtbazar").val(ui.item["namekamel"]);
                    $("#hfbazar").val(ui.item["id"]);
                }
            });
        }
    });

}


function loadcoding() {
    $.ajax({
        type: "POST",
        url: "/Main/getmoin",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#txtmoin,#txtmoinB,#txtmoinBE,#txtmoinE,#txtmoinkharj,#txtmoinsearch').autocomplete({
                source: data,
                select: function (event, ui) {
                    $("#namemoin,#namemoinB,#namemoinBE,#namemoinE , #namemoinkharj").html(ui.item["tafname"] + ' - ' + ui.item["moinname"]
                        + ' - ' + ui.item["kolname"]);
                    $("#hfmoinid,#hfmoinidB,#hfmoinidBE,#hfmoinidE,#hfmoinkharj,#hfmoinidsearch ").val(ui.item["id"].split("*")[0]);
                    $("#hftafid,#hftafidB,#hftafidBE,#hftafidE , #hftafkharj,#hftafidsearch").val(ui.item["id"].split("*")[1]);
                }
            });
        }
    });
    $.ajax({
        type: "POST",
        url: "/Main/getperson",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#txtpersonsearch, #txtperson,#txtpersonB,#txtpersonBE,#txtpersonE , #txtpersonkharj , #txtbazar').autocomplete({
                source: data,
                select: function (event, ui) {
                    $("#txtperson,#txtpersonB,#txtpersonBE,#txtpersonE , #txtpersonkharj ").html(ui.item["namekamel"]);
                    $("#txtbazar").val(ui.item["namekamel"]);
                    $("#hfpersonidsearch, #hfpersonid,#hfpersonidB,#hfpersonidBE,#hfpersonidE , #hfpersonkharj , #hfbazar" ).val(ui.item["id"]);
                }
            });
        }
    });
}
function loadperson2() {
    $.ajax({
        type: "POST",
        url: "/Main/getperson",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#txtperson2').autocomplete({
                source: data,
                select: function (event, ui) {
                    $("#hfpersonid2").val(ui.item["id"]);
                }
            });
        }
    });

}

function loadvisitor() {
    $.ajax({
        type: "POST",
        url: "/Main/getVisitor",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#txtvisit,#txtvisitE').autocomplete({
                source: data,
                select: function (event, ui) {
                    $("#txtvisit,#txtvisitE").html(ui.item["namekamel"]);
                    $("#hftxtvisitid,#txtvisitidE").val(ui.item["id"]);
                }
            });
        }
    });
}
function loadcodingBes() {
    $.ajax({
        type: "POST",
        url: "/Main/getmoin",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#txtmoinS,#txtmoinSE').autocomplete({
                source: data,
                select: function (event, ui) {
                    $("#namemoinS,#namemoinSE").html(ui.item["tafname"] + ' - ' + ui.item["moinname"]
                        + ' - ' + ui.item["kolname"]);
                    $("#hfmoinidS,#hfmoinidSE").val(ui.item["id"].split("*")[0]);
                    $("#hftafidS,#hftafidSE").val(ui.item["id"].split("*")[1]);
                }
            });
            //$('#txtmoinS,#txtmoinSE').autocomplete({
            //    source: data,
            //    select: function (event, ui) {
            //        $("#namemoinS,#namemoinSE").html(ui.item["moinname"] + ' - ' + ui.item["kolname"]);
            //        $("#hfmoinidS,#hfmoinidSE").val(ui.item["id"]);
            //    }
            //});
        }
    });
    //$.ajax({
    //    type: "POST",
    //    url: "/Main/gettaf",
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (data) {
    //        $('#txttafS,#txttafSE').autocomplete({
    //            source: data,
    //            select: function (event, ui) {
    //                $("#nametafS,#nametafSE").html(ui.item["nametaf"]);
    //                $("#hftafidS,#hftafidSE").val(ui.item["id"]);
    //            }
    //        });
    //    }
    //});
    $.ajax({
        type: "POST",
        url: "/Main/getperson",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#txtpersonS,#txtpersonSE').autocomplete({
                source: data,
                select: function (event, ui) {
                    $("#txtpersonS,#txtpersonSE").html(ui.item["namekamel"]);
                    $("#hfpersonidS,#hfpersonidSE").val(ui.item["id"]);
                }
            });
        }
    });
}

function loadanbar() {
    $.ajax({
        type: "POST",
        url: "/Main/getanbar",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.length > 0) {
                $("#txtanbar").val(data[0]["id"]);
                $("#nameanbar").html(data[0]["namec"]);
                $("#hfanbar").val(data[0]["id"]);
                
            }
            $('#txtanbar').autocomplete({
                source: data,
                select: function (event, ui) {
                    //$("#txtanbar").val(ui.item["mid"]);
                    $("#nameanbar").html(ui.item["namec"]);
                    $("#hfanbar").val(ui.item["mid"]);
                }
            });
        }
    });
}

function loadkala() {
    $('#txtcode').autocomplete({
        source: function (request, response) {
            var mform = new FormData();
            mform.append("str", $("#txtcode").val());
            $.ajax({
                type: "POST",
                url: "/Main/getkala",
                contentType: false,
                processData: false,
                data: mform,
                dataType: "json",
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.split('-')[0],
                            val: item.split('-')[1]
                        }
                    }))
                }
                //select: function (event, ui) {
                //    $("#namekala").html(ui.item["namekala"]);
                //    $("#hfcode").val(ui.item["mid"]);
                //    $("#hfkala").val(ui.item["codekala"]);
                //    $("#txtvahed").val(ui.item["vahed1"]);
                //    $("#txtpricef").val(ui.item["price2"]);
                //    $("#txtprice").val(ui.item["price1"]);

                //}
            });




            //var mform = new FormData();
            //mform.append("str", $("#txtcode").val());
            //$.ajax({
            //    type: "POST",
            //    url: "/Main/getkala",
            //    contentType: false,
            //    processData: false,
            //    data: mform,
            //    dataType: "json",
            //    success: function (data) {
            //        $('#txtcode,#myInputSearchmm').autocomplete({
            //            source: data,
            //            select: function (event, ui) {
            //                $("#namekala").html(ui.item["namekala"]);
            //                $("#hfcode").val(ui.item["mid"]);
            //                $("#hfkala").val(ui.item["codekala"]);
            //                $("#txtvahed").val(ui.item["vahed1"]);
            //                $("#txtpricef").val(ui.item["price2"]);
            //                $("#txtprice").val(ui.item["price1"]);

            //            }
            //        });
            //    }
            //});
        }
    })
}
function loadbarcode() {
    var mform = new FormData();
    mform.append("i", 1);
    $.ajax({
        type: "POST",
        url: "/Main/getkala",
        processData: false,
        contentType: false, //"application/json; charset=utf-8",
        dataType: "json",
        data: mform,
        success: function (data) {
            $('#myInputSearchmm').autocomplete({
                source: data,
                select: function (event, ui) {
                    $("#namekala").html(ui.item["namekala"]);
                    $("#hfcode").val(ui.item["mid"]);
                    $("#hfkala").val(ui.item["barcode"]);
                }
            });
        }
    });
}


function loadgapkala() {
    //$("#gidlist").val($("#" + q.id).val());
    $.ajax({
        type: "POST",
        url: "/Main/getcapkala?gid=" + $("#selgidc").val(),
        contentType: false,
        processData: false,
        dataType: "json",
        success: function (data) {
            if ($("#selgid").val() == -1) {
                $('#ck').val(data[0]["Mid"]);
            }
            else {
                var a2 = "اخرین کد گروه";
                var s = " کد های جا مانده کالا";
                a2 = a2 + data[0]["Mid"] + " ** "
                for (var i = 1; i < data.length; i++) {
                    s = s + data[i]["Mid"] + " - "
                }
                $('#ck').attr("placeholder", a2 + s);
                $('#ck').attr("title", a2 + s);
                $('#ck').val(data[0]["Mid"]);
            }
        }
    });
}

function loadpanel() {
    $.ajax({
        type: "POST",
        url: "/Main/getendpanel",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#txtpanel').autocomplete({
                source: data,
                minLength: 1000,
                select: function (event, ui) {
                    $("#hfpanelid").val(ui.item["mid"]);
                }
            });
        }
    });
}
function formatMoney(n, c, d, t) {
    var c = isNaN(c = Math.abs(c)) ? 2 : c,
        d = d == undefined ? "." : d,
        t = t == undefined ? "," : t,
        s = n < 0 ? "-" : "",
        i = String(parseInt(n = Math.abs(Number(n) || 0).toFixed(c))),
        j = (j = i.length) > 3 ? j % 3 : 0;

    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};




function generateBarcode() {
    var value = $("#hfbarcode").val();
    var btype = 'code93';
    var renderer = 'css';

    var settings = {
        output: renderer,
        bgColor: '#FFFFFF',
        color: '#000000',
        barWidth: '3',
        barHeight: '80',
        moduleSize: '5',
        posX: '10',
        posY: '20',
        fontSize: 17,
        addQuietZone: '1'
    };
    $(".barcodeTarget").barcode(value, btype, settings);
    setTimeout(500);
    $(".barcodeTarget").removeAttr("style");
    $(".barcodeTarget").addClass("barcodeTarget");
}

function sortTable(n, tid) {
    var table = $('#' + tid);

    $('.sortable th')
        .wrapInner('<span title="sort this column"/>')
        .each(function () {

            var th = $(this),
                thIndex = th.index(),
                inverse = false;

            th.click(function () {

                table.find('td').filter(function () {

                    return $(this).index() === thIndex;

                }).sortElements(function (a, b) {

                    if ($.text([a]) == $.text([b]))
                        return 0;

                    return $.text([a]) > $.text([b]) ?
                        inverse ? -1 : 1
                        : inverse ? 1 : -1;

                }, function () {

                    // parentNode is the element we want to move
                    return this.parentNode;

                });

                inverse = !inverse;

            });

        });
}

function chgardesh() {
    $("#hfisfact").val("0");
    if ($("#chbox").prop("checked") == true)
        $("#hfisfact").val("1");
}

function showfact(mid) {
    debugger;
    try {
        $(".modal-backdrop").removeClass("in");
        $("#Modalto").modal("hide");
        $("#Modalm").modal("hide");
    } catch (e) {

    }
    getfrm("/Factkalatbs/showfact?&id=" + mid, "فاکتور");
}

function getsanadfrm(nosanad) {
    $("#MyModal").modal("hide");
    getfrm("/Sanadtbs/Index?id=" + nosanad, "نمایش سند ش : " + nosanad);
}


function copyfact(mid) {
    var mform = new FormData();
    mform.append("mid", mid);
    /**/
    mform.append("typec", $("#hftypefact").val());
    /**/
    $.ajax({
        url: "/Facttbs/copyfact",
        type: "POST",
        contentType: false,
        processData: false,
        dataType: "json",
        data: mform,
        success: function (data) {
            if (data.state == 0) {
                alert(data.err);
                return;
            }
            var s = confirm('ایا میخواهید فاکتور ایجاد شده را ویرایش نمایید');
            if (s) {
                showfact(data.mid);
            }
        }
    })

}
function elhaghto() {
    $("#Modalto").modal("show");
}
function saveto() {
    var s = confirm('ایا مطمئن هستید ؟ ');
    if (s) {
        var typec = $("#seltypeto").val();
        var idfact = $("#hffactid").val();
        $.post('/Factkalatbs/saveto?id=' + idfact + '&typec=' + typec, function (data) {
            if (data.msg == "") {
                var s = confirm('فاکتور ذخیره شد ایا تمایل به نمایش و ویرایش فاکتور پیوستی هستید ؟')
                if (s) {
                    showfact(data.newidfact);
                }
                $("#hffactidtosave").val(data.newidfact);
                //$("#btnsaveto").show();        
            }
            else {
                $(".loading").hide();
                $(".errmsg").html(data.msg);
                $(".errdetail").html(data.err);
                $("#err").modal('show');
            }
        });
    }
}
function addperson() {
    $(".loading").show();
    $.ajax({
        url: "/Factkalatbs/addperson",
        type: "Get"
    }).done(function (result) {
        $("#MyModal2").modal("show");
        $("#ModalTittle2").html("ایجاد شخص");
        $("#ModalBody2").html(result);
        $(".loading").hide();
        
    });
}
