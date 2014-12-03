
    $(document).ready(function () {
            
        $("a[name*='lnkAddSubscription']").click(function () {
            //var me = $(this), data = me.data('params');
            var me = $(this);
            var page = parseInt(me.attr('page'));
            var topicId = parseInt(me.attr('topicId'));
            //AddSubscription(me.topicId, me.page);
        });

        function AddSubscription(topicId, page) {
            $.ajax({
                url: "/Subscription/AddSubscription",
                data: { topicId: topicId, page: page },
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).done(function () {
                toastr.success("Su subscripcion ha sido enviada!");
            }).error(function (data) {
                toastr.error("No se ah podido subscribir");
            });
        }

    });
