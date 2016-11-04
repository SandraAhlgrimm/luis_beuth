// Write your Javascript code.

function connectSelectorToElement(sel, el) {
    var toggle = function () {
        if (sel.value == -1) {
            $(el).show(200);
        } else {
            $(el).hide(200);
        }
    }

    sel.onchange = toggle;

    toggle();
}

