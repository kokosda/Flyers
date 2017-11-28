(function ($, w) {
	$(function () {
	    var getForm = function () {
	        return $("#content form");
	    };
	    var getTable = function () {
	        return getForm().find("div.table:visible:first").find("table:first");
	    }
	    var getCountiesSelectEl = function (stateSelectEl) {
	        return stateSelectEl.closest("tr").find("td:nth-child(7) select:visible");
	    };
	    var getAssociationsSelectEl = function (stateSelectEl) {
	        return stateSelectEl.closest("tr").find("td:nth-child(8) select:visible");
	    };
	    var getMsaSelectEl = function (stateSelectEl) {
	        return stateSelectEl.closest("tr").find("td:nth-child(9) select:visible");
	    };
	    var fillSelectEl = function (selectEl, values) {
	        var options = "<option value=''>[Select]</option>";

	        for (var v in values) {
	            options += "<option value='" + values[v] + "'>" + values[v] + "</option>";
	        }

	        selectEl.html(options);
	    };
	    var getHiddenScrollY = function () {
	        return getForm().find("[id*='hiddenScrollY']");
	    };
	    var hiddenScrollYHandlePostBack = function () {
	        $(".button").on("click", function () {
	            getHiddenScrollY().val(w.scrollY);
	        });
	    };
	    var setScroll = function () {
	        var hiddenScrollY = getHiddenScrollY();

	        if (hiddenScrollY.val().length > 0) {
	            var scrollY = parseInt(hiddenScrollY.val());

	            if (!isNaN(scrollY)) {
	                w.scrollTo(0, scrollY);
	            }
	        }
	    };

	    setScroll();
	    hiddenScrollYHandlePostBack();

	    getTable().find("tbody tr td:nth-child(6) select:visible").off().on("change", function () {
	        var targetEl = $(this);
	        var data = { state: "\"" + targetEl.val() + "\"" };

	        $.ajax({
	            url: getForm().data("marketsurl"),
	            type: "GET",
	            data: data,
                contentType: "application/json; charset=UTF-8",
	            success: function (r) {
	                var model = r.d;

	                if (model) {
	                    if (typeof model.Message == "string") {
	                        alert(model.Message);
	                    }
	                    else {
	                        var selectEl = getCountiesSelectEl(targetEl);

	                        selectEl.empty();
	                        fillSelectEl(selectEl, model.Counties);

	                        selectEl = getAssociationsSelectEl(targetEl);
	                        fillSelectEl(selectEl, model.Associations);

	                        selectEl = getMsaSelectEl(targetEl);
	                        fillSelectEl(selectEl, model.Msa);
	                    }
	                }
	            },
	            error: function () {
	                alert("Error while requesting markets for selectors.");
	            }
	        });
	    });
	});
})(jQuery, window);