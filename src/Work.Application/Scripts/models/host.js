(function ($) {

    function HostProtocolKoModel() {
    };

    function HostKoModel() {
    };

    $.extend(true, window, {
        Scope: {
            Host: HostKoModel,
            HostProtocol: HostProtocolKoModel
        }
    });

})(jQuery);