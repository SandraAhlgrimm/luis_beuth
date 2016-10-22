// Write your Javascript code.

function connectSelectorToElement(sel, el) {
    var toggle = function () {
        if (sel.value == -1) {
            el.style.display = "block";
        } else {
            el.style.display = "none";
        }
    }

    sel.onchange = toggle;

    toggle();
}

