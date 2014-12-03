
    $(document).ready(function () {
            
        $("a[name*='lnkAddSubscription']").click(function () {
            var me = $(this);
            var page = parseInt(me.attr('page'));
            var topicId = parseInt(me.attr('topicId'));
            AddSubscription(topicId, page);
        });

        function AddSubscription(topicId, page) {
            $.ajax({
                url: "/Subscription/AddSubscription",
                type: "POST",
                data: JSON.stringify({ topicId: topicId, page: page }),
                dataType: "json",
                contentType: 'application/json',
            }).done(function (data){
                toastr.success("Su subscripcion ha sido enviada!");
            }).error(function (data) {
                toastr.error("No se ah podido subscribir");
            });
        }

    });
