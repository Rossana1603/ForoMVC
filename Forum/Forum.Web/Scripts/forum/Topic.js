
    $(document).ready(function () {
            
        $("a[name*='lnkAddSubscription']").click(function () {
            var me = $(this);
            var page = parseInt(me.attr('page'));
            var topicId = parseInt(me.attr('topicId'));
            AddSubscription(topicId, page);
        });

        //function AddSubscription(topicId, page) {
        //    $.ajax({
        //        url: "/Subscription/AddSubscription",
        //        type: "POST",
        //        data: JSON.stringify({ topicId: topicId, page: page }),
        //        dataType: "json",
        //        contentType: 'application/json',
        //    }).done(function (){
        //        toastr.success("Su subscripcion ha sido enviada!");
        //    }).error(function (data) {
        //        toastr.error("No se ah podido subscribir");
        //    });
        //}
        var MessageModule = (function () {
            var optiontoastr = {
                "closeButton": true,
                "debug": false,
                "positionClass": "toast-bottom-full-width",
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "5000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut",
                "target": "div[id=ManagerContacts]"
            };
            toastr.option = optiontoastr;

            return {
                ShowMessage: function (message) {
                    toastr.error(message);
                },
                ShowSuccess: function (message, title) {
                    toastr.success(message, title);

                }

            }
        })();

        function AddSubscription(topicId, page) {
            var data = {
                topicId: topicId,
                page: page,
            };
            $.post("/Subscription/AddSubscription", data)
                .done(function () {
                    MessageModule.ShowSuccess("Su subscripcion ha sido enviada!");
                })
                .fail(function () {
                    MessageModule.ShowMessage("No se ah podido subscribir");
                });
        };});
