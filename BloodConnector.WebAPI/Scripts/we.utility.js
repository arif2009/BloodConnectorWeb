(function(We, $, undefined) {
    We.scroll = function (position) {
        if (position != undefined) {
            $(window).scrollTop(position);
        }

        return $(window).scrollTop();
    },
    We.parseQueryParams = function (query, keepRoute) {
        if (!query) {
            query = location.search.substr(1);
        }
        var result = {};
        query.split("&").forEach(function (part) {
            var item = part.split("=");
            if (item.length > 1) {
                var value = decodeURIComponent(item[1]);
                // remove routing from parameters
                if (keepRoute !== true)
                    value = value.replace(/#[^&]*/, "");

                // parameter values are case sensitive!!
                result[item[0].toLowerCase()] = value;
            }
        });
        return result;
    }
}(window.We = window.We || {}, jQuery));