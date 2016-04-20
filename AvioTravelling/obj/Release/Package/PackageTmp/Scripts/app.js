/// <reference path="jquery-2.2.1.js" />
/// <reference path="knockout-3.4.0.debug.js" />
/// <reference path="knockout.mapping-latest.debug.js" />


//Objkets App 
var App = {
    NotificationDuration: 1400,
    //Funkcija lai darit GET pieprasijumus
    //    url: Kur? piem: http://localhost/AvioTravelling/api/Geo/NewCityModel
    //    conevertToKnockout: 
    //    callback: ko izpidlit pec pieprasijuma, piemeram parnakt uiz citu lappusi
    //    requestDat: papild dati , ja niepecieshams, piem. EditCity(string Id) <- Id bus papildati 
    LoadData: function (url, conevertToKnockout, callback, requestData) {
       
        //jQuery: Pieprasijums ar jQuery bilbiotakas palidzibu
        $.ajax({
            method: "GET",  
            data: requestData,    // Parametri, ja nepiecieshamasm. piem EditCity(string Id) <- Id bus parameters
            url: url, 
            contentType: "application/json; UTF-8" // lai pateiktu serverim ka data bus JSON formata un kodeta ar UTF-8 kodejumu
        }).done(function (data) {
            if (conevertToKnockout) {
                callback(ko.mapping.fromJS(data));
            }
            else {
                callback(data);
            }

            App.UpdateHeights();
            $('input').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue',
              //  increaseArea: '20%' // optional
            });

        }).error(function (err, xhr, smth) {
            console.log('err', err);
            console.log('xhr', xhr);
            console.log('smth', smth);
        });
    },
    Navigate: function (url, parameters) {
        if (!parameters) {
            window.location.href = url;
            return;
        }

        if (url.indexOf('?') === -1) {
            window.location.href = url + "?" + $.param(parameters);
        }
        else {
            window.location.href = url + "&" + $.param(parameters);
        };
    },
    PostData: function (url, postData, action, showSuccessDialog) {

        var self = this;
        this.__showSuccessDialog = true;
        if (showSuccessDialog === void 0) {
            this.__showSuccessDialog = true;
        }

        else {
            this.__showSuccessDialog = showSuccessDialog;
        }

        $.ajax({
            method: "POST",
            data: JSON.stringify(ko.toJS(postData)),
            url: url,
            contentType: "application/json; UTF-8"
        }).done(function (data) {
            $(document).trigger('recordUpdated', [{ action: action }]);
            if (self.__showSuccessDialog) {
                App.SuccessBox();
            }
        });
    },
    SuccessBox: function (text) {
        if (!text) {
            text = "Record succcessfuly saved";
        }

        var timestamp = new Date().getTime();

        $('#notfications').prepend('<div Id="' + timestamp + '" class="alert alert-success alert-dismissible">                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>                <h4><i class="icon fa fa-check"></i> Success!</h4>                ' + text + '           </div>');
        setTimeout(function () {
            $('#' + timestamp).fadeOut("slow")
        }, App.NotificationDuration);

    },
    ErrorBox: function (text, Id) {

        //var modalWindow = null;
        var modals = $('.modal-dialog');
        //$.each(modals, function (idx, modal) {
        //    console.log('modal', modal);
        //    if ($(modal).data()['bs.modal'].isShown) {
        //        modalWindow = modal;
        //        return false;
        //    }
        //});



            $('#notfications').prepend('<div Id="' + Id + '" class="alert alert-danger alert-dismissible">                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>                <h4><i class="icon fa fa-check"></i> Error!</h4>                ' + text + '           </div>');
            $(modals).modal('hide');


        
        setTimeout(function () {
            $('#' + Id).fadeOut("slow");            
        }, App.NotificationDuration);

    },
    UpdateHeights: function () {
        $.each($('[data-updateHeight]'), function (idx, element) {
            $(element).height(element.scrollHeight);
        });
    },
    OpenModal: function (url, element, params) {
        params = params || {};
        $.ajax({
            url: url,
            data: params
        }).done(function (markup) {
            $(element).html(markup);
            $(element).modal('show');
        }).error(function (err, xhr, smth) {
            console.log('err', err);
            console.log('xhr', xhr);
            console.log('smth', smth);
        });
    },
    GetList: function (url, callback) {
        $.ajax({
            method: "GET",
            url: url,
            contentType: "application/json; UTF-8"
        }).done(function (data) {
            if (Array.isArray(data)) {
                var result = new Array();
                $.each(data, function (idx, record) {
                    result.push(ko.mapping.fromJS(record));
                });

                callback(result);
            }
            else {
                callback(ko.mapping.fromJS(result));
            }

        }).error(function (err, xhr, smth) {
            console.log('err', err);
            console.log('xhr', xhr);
            console.log('smth', smth);
        });
    },
};

$.ajaxSetup({
    error: function (xhr, textStatus, errorThrown) {
        var response = JSON.parse(xhr.responseText);
        
        if (response.ValidationErrors) {
            $.each(response.ValidationErrors, function (idx, validationFailure) {
                App.ErrorBox(validationFailure.Text, validationFailure.Id);
            });
        }

        else {
            App.ErrorBox(response.Message);
        }
    },
});



//$(document).on("recordUpdated", function () {

//})
