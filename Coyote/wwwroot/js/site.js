function setYear() { $("#currentYear").length && $("#currentYear").text((new Date).getFullYear().toString()) }

function MM_swapImgRestore() { var e, r, n = document.MM_sr; for (e = 0; n && e < n.length && (r = n[e]) && r.oSrc; e++) r.src = r.oSrc }

function MM_preloadImages() {
    var e = document;
    if (e.images) {
        e.MM_p || (e.MM_p = new Array);
        var r, n = e.MM_p.length,
            t = MM_preloadImages.arguments;
        for (r = 0; r < t.length; r++) 0 != t[r].indexOf("#") && (e.MM_p[n] = new Image, e.MM_p[n++].src = t[r])
    }
}

function MM_findObj(e, r) { var n, t, a; for (r || (r = document), (n = e.indexOf("?")) > 0 && parent.frames.length && (r = parent.frames[e.substring(n + 1)].document, e = e.substring(0, n)), !(a = r[e]) && r.all && (a = r.all[e]), t = 0; !a && t < r.forms.length; t++) a = r.forms[t][e]; for (t = 0; !a && r.layers && t < r.layers.length; t++) a = MM_findObj(e, r.layers[t].document); return !a && r.getElementById && (a = r.getElementById(e)), a }

function MM_swapImage() {
    var e, r, n = 0,
        t = MM_swapImage.arguments;
    for (document.MM_sr = new Array, e = 0; e < t.length - 2; e += 3) null != (r = MM_findObj(t[e])) && (document.MM_sr[n++] = r, r.oSrc || (r.oSrc = r.src), r.src = t[e + 2])
}
window.onload = setYear;