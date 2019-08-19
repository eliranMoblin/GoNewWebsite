﻿/*!
 * Start Bootstrap - Agency v5.2.1 (https://startbootstrap.com/template-overviews/agency)
 * Copyright 2013-2019 Start Bootstrap
 * Licensed under MIT (https://github.com/BlackrockDigital/startbootstrap-agency/blob/master/LICENSE)
 */

!function (e) { "use strict"; e('a.js-scroll-trigger[href*="#"]:not([href="#"])').click(function () { if (location.pathname.replace(/^\//, "") == this.pathname.replace(/^\//, "") && location.hostname == this.hostname) { var a = e(this.hash); if ((a = a.length ? a : e("[name=" + this.hash.slice(1) + "]")).length) return e("html, body").animate({ scrollTop: a.offset().top - 54 }, 1e3, "easeInOutExpo"), !1 } }), e(".js-scroll-trigger").click(function () { e(".navbar-collapse").collapse("hide") }), e("body").scrollspy({ target: "#mainNav", offset: 56 }); function a() { 100 < e("#mainNav").offset().top ? e("#mainNav").addClass("navbar-shrink") : e("#mainNav").removeClass("navbar-shrink") } a(), e(window).scroll(a) }(jQuery);
/*! jQuery.Flipster, v1.1.3 (built 2019-05-01) */

//!function (I, t, P) { "use strict"; function D(n, i) { var a = null; return function () { var t = this, e = arguments; null === a && (a = setTimeout(function () { n.apply(t, e), a = null }, i)) } } var r, e = (r = {}, function (t) { if (r[t] !== P) return r[t]; var e = document.createElement("div").style, n = t.charAt(0).toUpperCase() + t.slice(1), i = (t + " " + ["webkit", "moz", "ms", "o"].join(n + " ") + n).split(" "); for (var a in i) if (i[a] in e) return r[t] = i[a]; return r[t] = !1 }), L = "http://www.w3.org/2000/svg", E = I(t), M = e("transform"), i = { itemContainer: "ul", itemSelector: "li", start: "center", fadeIn: 400, loop: !1, autoplay: !1, pauseOnHover: !0, style: "coverflow", spacing: -.6, click: !0, keyboard: !0, scrollwheel: !0, touch: !0, nav: !1, buttons: !1, buttonPrev: "Previous", buttonNext: "Next", onItemSwitch: !1 }, T = { main: "flipster", active: "flipster--active", container: "flipster__container", nav: "flipster__nav", navChild: "flipster__nav__child", navItem: "flipster__nav__item", navLink: "flipster__nav__link", navCurrent: "flipster__nav__item--current", navCategory: "flipster__nav__item--category", navCategoryLink: "flipster__nav__link--category", button: "flipster__button", buttonPrev: "flipster__button--prev", buttonNext: "flipster__button--next", item: "flipster__item", itemCurrent: "flipster__item--current", itemPast: "flipster__item--past", itemFuture: "flipster__item--future", itemContent: "flipster__item__content" }, X = new RegExp("\\b(" + T.itemCurrent + "|" + T.itemPast + "|" + T.itemFuture + ")(.*?)(\\s|$)", "g"), j = new RegExp("\\s\\s+", "g"); I.fn.flipster = function (e) { if ("string" == typeof e) { var n = Array.prototype.slice.call(arguments, 1); return this.each(function () { var t = I(this).data("methods"); return t[e] ? t[e].apply(this, n) : this }) } var k = I.extend({}, i, e); return this.each(function () { var t, r, o, e, l, s, c, u, f, i = I(this), p = [], v = 0, a = !1, n = !1; function h(e) { return e = e || "next", I('<button class="' + T.button + " " + ("next" === e ? T.buttonNext : T.buttonPrev) + '" role="button" />').html(function (t) { var e = "next" === t ? k.buttonNext : k.buttonPrev; return "custom" === k.buttons ? e : '<svg viewBox="0 0 13 20" xmlns="' + L + '" aria-labelledby="title"><title>' + e + '</title><polyline points="10,3 3,10 10,17"' + ("next" === t ? ' transform="rotate(180 6.5,10)"' : "") + "/></svg>" }(e)).on("click", function (t) { _(e), t.preventDefault() }) } function d() { i.css("transition", ""), r.css("transition", ""), l.css("transition", "") } function m(a) { a && (i.css("transition", "none"), r.css("transition", "none"), l.css("transition", "none")), o = r.width(), r.height(function () { var t, e = 0; return l.each(function () { t = I(this).height(), e < t && (e = t) }), e }()), o ? (e && (clearInterval(e), e = !1), l.each(function (t) { var e, n, i = I(this); i.attr("class", function (t, e) { return e && e.replace(X, "").replace(j, " ") }), e = i.outerWidth(), 0 !== k.spacing && i.css("margin-right", e * k.spacing + "px"), n = i.position().left, p[t] = -1 * (n + e / 2 - o / 2), t === l.length - 1 && (g(), a && setTimeout(d, 1)) })) : e = e || setInterval(function () { m(a) }, 500) } function g() { var e, n, i, a = l.length; l.each(function (t) { e = I(this), n = " ", i = t === v ? (n += T.itemCurrent, a + 1) : t < v ? (n += T.itemPast + " " + T.itemPast + "-" + (v - t), a - (v - t)) : (n += T.itemFuture + " " + T.itemFuture + "-" + (t - v), a - (t - v)), e.css("z-index", i).attr("class", function (t, e) { return e && e.replace(X, "").replace(j, " ") + n }) }), 0 <= v && (o && p[v] !== P || m(!0), M ? r.css("transform", "translateX(" + p[v] + "px)") : r.css({ left: p[v] + "px" })), function () { if (k.nav) { var t = s.data("flip-category"); u.removeClass(T.navCurrent), f.filter(function () { return I(this).data("index") === v || t && I(this).data("category") === t }).parent().addClass(T.navCurrent) } }() } function _(t) { var e = v; if (!(l.length <= 1)) return "prev" === t ? 0 < v ? v-- : k.loop && (v = l.length - 1) : "next" === t ? v < l.length - 1 ? v++ : k.loop && (v = 0) : "number" == typeof t ? v = t : t !== P && (v = l.index(t), k.loop && e != v && (e == l.length - 1 && v != l.length - 2 && (v = 0), 0 == e && 1 != v && (v = l.length - 1))), s = l.eq(v), v !== e && k.onItemSwitch && k.onItemSwitch.call(i, l[v], l[e]), g(), i } function y(t) { return k.autoplay = t || k.autoplay, clearInterval(a), a = setInterval(function () { var t = v; _("next"), t !== v || k.loop || clearInterval(a) }, k.autoplay), i } function b() { return clearInterval(a), a = 0, i } function x(t) { return b(), k.autoplay && t && (a = -1), i } function w() { m(!0), i.hide().css("visibility", "").addClass(T.active).fadeIn(k.fadeIn) } function C() { if (r = i.find(k.itemContainer).addClass(T.container), !((l = r.find(k.itemSelector)).length <= 1)) return l.addClass(T.item).each(function () { var t = I(this); t.children("." + T.itemContent).length || t.wrapInner('<div class="' + T.itemContent + '" />') }), k.click && l.on("click.flipster touchend.flipster", function (t) { n || (I(this).hasClass(T.itemCurrent) || t.preventDefault(), _(this)) }), k.buttons && 1 < l.length && (i.find("." + T.button).remove(), i.append(h("prev"), h("next"))), function () { var s = {}; !k.nav || l.length <= 1 || (c && c.remove(), c = I('<ul class="' + T.nav + '" role="navigation" />'), f = I(""), l.each(function (t) { var e = I(this), n = e.data("flip-category"), i = e.data("flip-title") || e.attr("title") || t, a = I('<a href="#" class="' + T.navLink + '">' + i + "</a>").data("index", t); if (f = f.add(a), n) { if (!s[n]) { var r = I('<li class="' + T.navItem + " " + T.navCategory + '">'), o = I('<a href="#" class="' + T.navLink + " " + T.navCategoryLink + '" data-flip-category="' + n + '">' + n + "</a>").data("category", n).data("index", t); s[n] = I('<ul class="' + T.navChild + '" />'), f = f.add(o), r.append(o, s[n]).appendTo(c) } s[n].append(a) } else c.append(a); a.wrap('<li class="' + T.navItem + '">') }), c.on("click", "a", function (t) { var e = I(this).data("index"); 0 <= e && (_(e), t.preventDefault()) }), "after" === k.nav ? i.append(c) : i.prepend(c), u = c.find("." + T.navItem)) }(), 0 <= v && _(v), i } t = { jump: _, next: function () { return _("next") }, prev: function () { return _("prev") }, play: y, stop: b, pause: x, index: C }, i.data("methods", t), i.hasClass(T.active) || function () { var t; if (i.css("visibility", "hidden"), C(), l.length <= 1) i.css("visibility", ""); else { t = !!k.style && "flipster--" + k.style.split(" ").join(" flipster--"), i.addClass([T.main, M ? "flipster--transform" : " flipster--no-transform", t, k.click ? "flipster--click" : ""].join(" ")), k.start && (v = "center" === k.start ? Math.floor(l.length / 2) : k.start), _(v); var e = i.find("img"); if (e.length) { var n = 0; e.on("load", function () { ++n >= e.length && w() }), setTimeout(w, 750) } else w(); E.on("resize.flipster", D(m, 400)), k.autoplay && y(), k.pauseOnHover && r.on("mouseenter.flipster", function () { a ? x(!0) : b() }).on("mouseleave.flipster", function () { -1 === a && y() }), function (t) { k.keyboard && (t[0].tabIndex = 0, t.on("keydown.flipster", D(function (t) { var e = t.which; 37 !== e && 39 !== e || (_(37 === e ? "prev" : "next"), t.preventDefault()) }, 250))) }(i), function (t) { if (k.scrollwheel) { var e, n, i = !1, a = 0, r = 0, o = 0, s = /mozilla/.test(navigator.userAgent.toLowerCase()) && !/webkit/.test(navigator.userAgent.toLowerCase()); t.on("mousewheel.flipster wheel.flipster", function () { i = !0 }).on("mousewheel.flipster wheel.flipster", D(function (t) { clearTimeout(r), r = setTimeout(function () { o = a = 0 }, 300), t = t.originalEvent, o += t.wheelDelta || -1 * (t.deltaY + t.deltaX), Math.abs(o) < 25 && !s || (a++ , n !== (e = 0 < o ? "prev" : "next") && (a = 0), n = e, (a < 6 || a % 3 == 0) && _(e), o = 0) }, 50)), E.on("mousewheel.flipster wheel.flipster", function (t) { i && (t.preventDefault(), i = !1) }) } }(r), function (t) { var e, n, i, a, r, o; k.touch && t.on({ "touchstart.flipster": function (t) { t = t.originalEvent, e = t.touches ? t.touches[0].clientX : t.clientX, n = t.touches ? t.touches[0].clientY : t.clientY }, "touchmove.flipster": function (t) { t = t.originalEvent, i = t.touches ? t.touches[0].clientX : t.clientX, a = t.touches ? t.touches[0].clientY : t.clientY, o = i - e, r = a - n, 30 < Math.abs(o) && Math.abs(r) < 100 && t.preventDefault() }, "touchend.flipster touchcancel.flipster ": function () { o = i - e, r = a - n, 30 < Math.abs(o) && Math.abs(r) < 100 && _(0 < o ? "prev" : "next") } }) }(r) } }() }) } }(jQuery, window);
(function ($) {
    $.fn.slidingCarousel = function (options) {
        options = $.extend({}, $.fn.slidingCarousel.defaults, options || {});

        var pluginData = {
            container: $(this),
            sinus: [0],
            images: null,
            mIndex: null
        };

        var preload = function (callback) {
            var images = pluginData.container.find('.slide .triangle-right'),
                total = images.length,
                shift = total % 2,
                middle = total < 3 ? total : ~~(total / 2) + shift,
                result = [],
                loaded = 0;

            console.log(total);
            images.each(function (index, element) {
                var img = new Image();


                console.log(element);
                $(img).bind('load error', function () {
                    loaded++;

                    // push loaded images into regular array.
                    // to preserve the order array gets constructed so, that elements indexed:
                    //
                    //    0 1 2 3 4 5 6
                    //
                    // are shifted within destination array by half of total length (-1 depending on parity/oddness):
                    //
                    //    6 5 4 0 1 2 3
                    //
                    // and finally reversed to get correct scrolling direction.

                    result[(index + middle - shift) % total] = element;

                    // need ratio for calculating new widths
                    element.ratio = this.width / this.height;
                    element.origH = this.height;
                    element.idx = index;

                    if (loaded == total) {
                        pluginData.mIndex = middle;
                        pluginData.sinsum = 0;
                        pluginData.images = result.reverse();

                        // prepare symetric sinus table

                        for (var n = 1, freq = 0; n < total; n++) {
                            pluginData.sinus[n] = (n <= middle) ? Math.sin(freq += (1.6 / middle)) : pluginData.sinus[total - n];

                            if (n < middle)
                                pluginData.sinsum += pluginData.sinus[n] * options.squeeze;
                        }
                        callback(pluginData.images);
                    }
                });
                img.src = element.src;
            });
        };

        var setupCarousel = function () {
            preload(doLayout);
            setupEvents();
        };

        var setupEvents = function () {
            pluginData.container
                .find('.slide p > a').click(function (e) {
                    var idx = $(this).find('.triangle-right')[0].idx,
                        arr = pluginData.images;

                    while (arr[pluginData.mIndex - 1].idx != idx) {
                        arr.push(arr.shift());
                    }
                    doLayout(arr, true);
                })
                .end()
                .find('.navigate-right').click(function () {
                    var images = pluginData.images;

                    images.splice(0, 0, images.pop());
                    doLayout(images, true);
                })
                .end()
                .find('.navigate-left').click(function () {
                    var images = pluginData.images;

                    images.push(images.shift());
                    doLayout(images, true);
                });
        };

        var doLayout = function (images, animate) {
            var mid = pluginData.mIndex,
                sin = pluginData.sinus,
                posx = 0,
                diff = 0,
                width = images[mid - 1].origH * images[mid - 1].ratio,  /* width of middle picture */
                middle = (pluginData.container.width() - width) / 2,   /* center of middle picture, relative to container */
                offset = middle - pluginData.sinsum,                 /* to get the center, all sinus offset that will be added below have to be substracted */
                height = images[mid - 1].origH, top, left, idx, j = 1;

            // hide description before doing layout
            pluginData.container.find('span').hide().css('opacity', 0);

            $.each(images, function (i, img) {
                idx = Math.abs(i + 1 - mid);
                top = idx * 15;

                // calculating new width and caching it for later use
                img.cWidth = (height - (top * 2)) * img.ratio;

                diff = sin[i] * options.squeeze;
                left = posx += (i < mid) ? diff : images[i - 1].cWidth + diff - img.cWidth;

                var el = $(img).closest('.slide'),
                    fn = function () {
                        if (i === mid - 1) {
                            // show caption gently
                            el.find('span').show().animate({ opacity: 0.7 });
                        }
                    };

                if (animate) {
                    el.animate({
                        height: height - (top * 2),
                        zIndex: mid - idx,
                        top: top,
                        left: left + offset,
                        opacity: i == mid - 1 ? 1 : sin[j++] * 0.8
                    }, options.animate, fn);
                }
                else {
                    el.css({
                        zIndex: mid - idx,
                        height: height - (top * 2),
                        top: top,
                        left: left + offset,
                        opacity: 0
                    }).show().animate({ opacity: i == mid - 1 ? 1 : sin[j++] * 0.8 }, fn);

                    if (options.shadow) {
                        el.addClass('shadow');
                    }
                }
            });

            if (!animate) {
                pluginData.container
                    .find('.navigate-left').css('left', middle + 50)
                    .end()
                    .find('.navigate-right').css('left', middle + width - 75);
            }
        };

        this.initialize = function () {
            setupCarousel();
        };

        // Initialize the plugin
        return this.initialize();

    };

    $.fn.slidingCarousel.defaults = {
        shadow: true,
        squeeze: 124,
        animate: 250
    };

})(jQuery);


/* jshint browser: true, jquery: true, devel: true */
/* global window, jQuery */

(function ($, window, undefined) {
    'use strict';

    function throttle(func, delay) {
        var timer = null;

        return function () {
            var context = this,
                args = arguments;

            if (timer === null) {
                timer = setTimeout(function () {
                    func.apply(context, args);
                    timer = null;
                }, delay);
            }
        };
    }

    // Check for browser CSS support and cache the result for subsequent calls.
    var checkStyleSupport = (function () {
        var support = {};
        return function (prop) {
            if (support[prop] !== undefined) { return support[prop]; }

            var div = document.createElement('div'),
                style = div.style,
                ucProp = prop.charAt(0).toUpperCase() + prop.slice(1),
                prefixes = ["webkit", "moz", "ms", "o"],
                props = (prop + ' ' + (prefixes).join(ucProp + ' ') + ucProp).split(' ');

            for (var i in props) {
                if (props[i] in style) { return support[prop] = props[i]; }
            }

            return support[prop] = false;
        };
    }());

    var svgNS = 'http://www.w3.org/2000/svg',
        svgSupport = (function () {
            var support;
            return function () {
                if (support !== undefined) { return support; }
                var div = document.createElement('div');
                div.innerHTML = '<svg/>';
                support = (div.firstChild && div.firstChild.namespaceURI === svgNS);
                return support;
            };
        }());

    var $window = $(window),

        transformSupport = checkStyleSupport('transform'),

        defaults = {
            itemContainer: 'ul',
            // [string|object]
            // Selector for the container of the flippin' items.

            itemSelector: 'li',
            // [string|object]
            // Selector for children of `itemContainer` to flip

            start: 'center',
            // ['center'|number]
            // Zero based index of the starting item, or use 'center' to start in the middle

            fadeIn: 400,
            // [milliseconds]
            // Speed of the fade in animation after items have been setup

            loop: false,
            // [true|false|number]
            // Loop around when the start or end is reached
            // If number, this is the number of items that will be shown when the beginning or end is reached

            autoplay: false,
            // [false|milliseconds]
            // If a positive number, Flipster will automatically advance to next item after that number of milliseconds

            pauseOnHover: true,
            // [true|false]
            // If true, autoplay advancement will pause when Flipster is hovered

            style: 'coverflow',
            // [coverflow|carousel|flat|...]
            // Adds a class (e.g. flipster--coverflow) to the flipster element to switch between display styles
            // Create your own theme in CSS and use this setting to have Flipster add the custom class

            spacing: -0.6,
            // [number]
            // Space between items relative to each item's width. 0 for no spacing, negative values to overlap

            click: true,
            // [true|false]
            // Clicking an item switches to that item

            keyboard: true,
            // [true|false]
            // Enable left/right arrow navigation

            scrollwheel: true,
            // [true|false]
            // Enable mousewheel/trackpad navigation; up/left = previous, down/right = next

            touch: true,
            // [true|false]
            // Enable swipe navigation for touch devices

            nav: false,
            // [true|false|'before'|'after']
            // If not false, Flipster will build an unordered list of the items
            // Values true or 'before' will insert the navigation before the items, 'after' will append the navigation after the items

            buttons: false,
            // [true|false|'custom']
            // If true, Flipster will insert Previous / Next buttons with SVG arrows
            // If 'custom', Flipster will not insert the arrows and will instead use the values of `buttonPrev` and `buttonNext`

            buttonPrev: 'Previous',
            // [text|html]
            // Changes the text for the Previous button

            buttonNext: 'Next',
            // [text|html]
            // Changes the text for the Next button

            onItemSwitch: false
            // [function]
            // Callback function when items are switched
            // Arguments received: [currentItem, previousItem]
        },

        classes = {
            main: 'flipster',
            active: 'flipster--active',
            container: 'flipster__container',

            nav: 'flipster__nav',
            navChild: 'flipster__nav__child',
            navItem: 'flipster__nav__item',
            navLink: 'flipster__nav__link',
            navCurrent: 'flipster__nav__item--current',
            navCategory: 'flipster__nav__item--category',
            navCategoryLink: 'flipster__nav__link--category',

            button: 'flipster__button',
            buttonPrev: 'flipster__button--prev',
            buttonNext: 'flipster__button--next',

            item: 'flipster__item',
            itemCurrent: 'flipster__item--current',
            itemPast: 'flipster__item--past',
            itemFuture: 'flipster__item--future',
            itemContent: 'flipster__item__content'
        },

        classRemover = new RegExp('\\b(' + classes.itemCurrent + '|' + classes.itemPast + '|' + classes.itemFuture + ')(.*?)(\\s|$)', 'g'),
        whiteSpaceRemover = new RegExp('\\s\\s+', 'g');

    $.fn.flipster = function (options) {
        var isMethodCall = (typeof options === 'string' ? true : false);

        if (isMethodCall) {
            var args = Array.prototype.slice.call(arguments, 1);
            return this.each(function () {
                var methods = $(this).data('methods');
                if (methods[options]) {
                    return methods[options].apply(this, args);
                } else {
                    return this;
                }
            });
        }

        var settings = $.extend({}, defaults, options);

        return this.each(function () {

            var self = $(this),
                methods,

                _container,
                _containerWidth,

                _items,
                _itemOffsets = [],
                _currentItem,
                _currentIndex = 0,

                _nav,
                _navItems,
                _navLinks,

                _playing = false,
                _startDrag = false;

            function buildButtonContent(dir) {
                var text = (dir === 'next' ? settings.buttonNext : settings.buttonPrev);

                if (settings.buttons === 'custom' || !svgSupport) { return text; }

                return '<svg viewBox="0 0 13 20" xmlns="' + svgNS + '" aria-labelledby="title"><title>' + text + '</title><polyline points="10,3 3,10 10,17"' + (dir === 'next' ? ' transform="rotate(180 6.5,10)"' : '') + '/></svg>';
            }

            function buildButton(dir) {
                dir = dir || 'next';

                return $('<button class="' + classes.button + ' ' + (dir === 'next' ? classes.buttonNext : classes.buttonPrev) + '" role="button" />')
                    .html(buildButtonContent(dir))
                    .on('click', function (e) {
                        jump(dir);
                        e.preventDefault();
                    });

            }

            function buildButtons() {
                if (settings.buttons && _items.length > 1) {
                    self.find('.' + classes.button).remove();
                    self.append(buildButton('prev'), buildButton('next'));
                }
            }

            function buildNav() {
                var navCategories = {};

                if (!settings.nav || _items.length <= 1) { return; }

                if (_nav) { _nav.remove(); }

                _nav = $('<ul class="' + classes.nav + '" role="navigation" />');
                _navLinks = $('');

                _items.each(function (i) {
                    var item = $(this),
                        category = item.data('flip-category'),
                        itemTitle = item.data('flip-title') || item.attr('title') || i,
                        navLink = $('<a href="#" class="' + classes.navLink + '">' + itemTitle + '</a>')
                            .data('index', i);

                    _navLinks = _navLinks.add(navLink);

                    if (category) {

                        if (!navCategories[category]) {

                            var categoryItem = $('<li class="' + classes.navItem + ' ' + classes.navCategory + '">');
                            var categoryLink = $('<a href="#" class="' + classes.navLink + ' ' + classes.navCategoryLink + '" data-flip-category="' + category + '">' + category + '</a>')
                                .data('category', category)
                                .data('index', i);

                            navCategories[category] = $('<ul class="' + classes.navChild + '" />');

                            _navLinks = _navLinks.add(categoryLink);

                            categoryItem
                                .append(categoryLink, navCategories[category])
                                .appendTo(_nav);
                        }

                        navCategories[category].append(navLink);
                    } else {
                        _nav.append(navLink);
                    }

                    navLink.wrap('<li class="' + classes.navItem + '">');

                });

                _nav.on('click', 'a', function (e) {
                    var index = $(this).data('index');
                    if (index >= 0) {
                        jump(index);
                        e.preventDefault();
                    }
                });

                if (settings.nav === 'after') { self.append(_nav); }
                else { self.prepend(_nav); }

                _navItems = _nav.find('.' + classes.navItem);
            }

            function updateNav() {
                if (settings.nav) {

                    var category = _currentItem.data('flip-category');

                    _navItems.removeClass(classes.navCurrent);

                    console.log(_navLinks
                        .filter(function () {
                            return ($(this).data('index') === _currentIndex || (category && $(this).data('category') === category));
                        }));
                    _navLinks
                        .filter(function () {
                            return ($(this).data('index') === _currentIndex || (category && $(this).data('category') === category));
                        })
                        .parent()
                        .addClass(classes.navCurrent);

                }
            }

            function noTransition() {
                self.css('transition', 'none');
                _container.css('transition', 'none');
                _items.css('transition', 'none');
            }

            function resetTransition() {
                self.css('transition', '');
                _container.css('transition', '');
                _items.css('transition', '');
            }

            function calculateBiggestItemHeight() {
                var biggestHeight = 0,
                    itemHeight;

                _items.each(function () {
                    itemHeight = $(this).height();
                    if (itemHeight > biggestHeight) { biggestHeight = itemHeight; }
                });
                return biggestHeight;
            }

            function resize(skipTransition) {
                if (skipTransition) { noTransition(); }

                _containerWidth = _container.width();
                _container.height(calculateBiggestItemHeight());

                _items.each(function (i) {
                    var item = $(this),
                        width,
                        left;

                    item.attr('class', function (i, c) {
                        return c && c.replace(classRemover, '').replace(whiteSpaceRemover, ' ');
                    });

                    width = item.outerWidth();

                    if (settings.spacing !== 0) {
                        item.css('margin-right', (width * settings.spacing) + 'px');
                    }

                    left = item.position().left;
                    _itemOffsets[i] = -1 * ((left + (width / 2)) - (_containerWidth / 2));

                    if (i === _items.length - 1) {
                        center();
                        if (skipTransition) { setTimeout(resetTransition, 1); }
                    }
                });
            }

            function center() {
                var total = _items.length,
                    loopCount = (settings.loop !== true && settings.loop > 0 ? settings.loop : false),
                    item, newClass, zIndex, past, offset;

                if (_currentIndex >= 0) {

                    _items.each(function (i) {
                        item = $(this);
                        newClass = ' ';

                        if (i === _currentIndex) {
                            newClass += classes.itemCurrent;
                            zIndex = (total + 2);
                        } else {
                            past = (i < _currentIndex ? true : false);
                            offset = (past ? _currentIndex - i : i - _currentIndex);

                            if (loopCount) {
                                if (_currentIndex <= loopCount && i > _currentIndex + loopCount) {
                                    past = true;
                                    offset = (total + _currentIndex) - i;
                                } else if (_currentIndex >= total - loopCount && i < _currentIndex - loopCount) {
                                    past = false;
                                    offset = (total - _currentIndex) + i;
                                }
                            }

                            newClass += (past ?
                                classes.itemPast + ' ' + classes.itemPast + '-' + offset :
                                classes.itemFuture + ' ' + classes.itemFuture + '-' + offset
                            );

                            zIndex = total - offset;
                        }

                        item
                            .css('z-index', zIndex * 2)
                            .attr('class', function (i, c) {
                                return c && c.replace(classRemover, '').replace(whiteSpaceRemover, ' ') + newClass;
                            });
                    });

                    if (!_containerWidth || _itemOffsets[_currentIndex] === undefined) { resize(true); }

                    if (transformSupport) {
                        _container.css('transform', 'translateX(' + _itemOffsets[_currentIndex] + 'px)');
                    } else {
                        _container.css('left', _itemOffsets[_currentIndex] + 'px');
                    }
                }

                updateNav();
            }

            function jump(to) {
                var _previous = _currentIndex;

                if (_items.length <= 1) { return; }

                if (to === 'prev') {
                    if (_currentIndex > 0) { _currentIndex--; }
                    else if (settings.loop) { _currentIndex = _items.length - 1; }
                } else if (to === 'next') {
                    if (_currentIndex < _items.length - 1) { _currentIndex++; }
                    else if (settings.loop) { _currentIndex = 0; }
                } else if (typeof to === 'number') {
                    _currentIndex = to;
                } else if (to !== undefined) {
                    // if object is sent, get its index
                    _currentIndex = _items.index(to);
                }

                _currentItem = _items.eq(_currentIndex);

                if (_currentIndex !== _previous && settings.onItemSwitch) {
                    settings.onItemSwitch.call(self, _items[_currentIndex], _items[_previous]);
                }

                center();

                return self;
            }

            function play(interval) {
                settings.autoplay = interval || settings.autoplay;

                clearInterval(_playing);

                _playing = setInterval(function () {
                    var prev = _currentIndex;
                    jump('next');
                    if (prev === _currentIndex && !settings.loop) { clearInterval(_playing); }
                }, settings.autoplay);

                return self;
            }

            function pause() {
                clearInterval(_playing);
                if (settings.autoplay) { _playing = -1; }

                return self;
            }

            function show() {
                resize(true);
                self.hide()
                    .css('visibility', '')
                    .addClass(classes.active)
                    .fadeIn(settings.fadeIn);
            }

            function index() {

                _container = self.find(settings.itemContainer).addClass(classes.container);

                _items = _container.find(settings.itemSelector);

                if (_items.length <= 1) { return; }

                _items
                    .addClass(classes.item)
                    // Wrap inner content
                    .each(function () {
                        var item = $(this);
                        if (!item.children('.' + classes.itemContent).length) {
                            item.wrapInner('<div class="' + classes.itemContent + '" />');
                        }
                    });

                // Navigate directly to an item by clicking
                if (settings.click) {
                    _items.on('click.flipster touchend.flipster', function (e) {
                        if (!_startDrag) {
                            if (!$(this).hasClass(classes.itemCurrent)) { e.preventDefault(); }
                            jump(this);
                        }
                    });
                }

                // Insert navigation if enabled.
                buildButtons();
                buildNav();

                if (_currentIndex >= 0) { jump(_currentIndex); }

                return self;
            }

            function keyboardEvents(elem) {
                if (settings.keyboard) {
                    elem[0].tabIndex = 0;
                    elem.on('keydown.flipster', throttle(function (e) {
                        var code = e.which;
                        if (code === 37 || code === 39) {
                            jump(code === 37 ? 'prev' : 'next');
                            e.preventDefault();
                        }
                    }, 250, true));
                }
            }

            function wheelEvents(elem) {
                if (settings.scrollwheel) {
                    var _wheelInside = false,
                        _actionThrottle = 0,
                        _throttleTimeout = 0,
                        _delta = 0,
                        _dir, _lastDir;

                    elem
                        .on('mousewheel.flipster wheel.flipster', function () { _wheelInside = true; })
                        .on('mousewheel.flipster wheel.flipster', throttle(function (e) {

                            // Reset after a period without scrolling.
                            clearTimeout(_throttleTimeout);
                            _throttleTimeout = setTimeout(function () {
                                _actionThrottle = 0;
                                _delta = 0;
                            }, 300);

                            e = e.originalEvent;

                            // Add to delta (+=) so that continuous small events can still get past the speed limit, and quick direction reversals get cancelled out
                            _delta += (e.wheelDelta || (e.deltaY + e.deltaX) * -1); // Invert numbers for Firefox

                            // Don't trigger unless the scroll is decent speed.
                            if (Math.abs(_delta) < 25) { return; }

                            _actionThrottle++;

                            _dir = (_delta > 0 ? 'prev' : 'next');

                            // Reset throttle if direction changed.
                            if (_lastDir !== _dir) { _actionThrottle = 0; }
                            _lastDir = _dir;

                            // Regular scroll wheels trigger less events, so they don't need to be throttled. Trackpads trigger many events (inertia), so only trigger jump every three times to slow things down.
                            if (_actionThrottle < 6 || _actionThrottle % 3 === 0) { jump(_dir); }

                            _delta = 0;

                        }, 50));

                    // Disable mousewheel on window if event began in elem.
                    $window.on('mousewheel.flipster wheel.flipster', function (e) {
                        if (_wheelInside) {
                            e.preventDefault();
                            _wheelInside = false;
                        }
                    });
                }
            }

            function touchEvents(elem) {
                if (settings.touch) {
                    var _startDragY = false,
                        _touchJump = throttle(jump, 300),
                        x, y, offsetY, offsetX;

                    elem.on({
                        'touchstart.flipster': function (e) {
                            e = e.originalEvent;
                            _startDrag = (e.touches ? e.touches[0].clientX : e.clientX);
                            _startDragY = (e.touches ? e.touches[0].clientY : e.clientY);
                            //e.preventDefault();
                        },

                        'touchmove.flipster': throttle(function (e) {
                            if (_startDrag !== false) {
                                e = e.originalEvent;

                                x = (e.touches ? e.touches[0].clientX : e.clientX);
                                y = (e.touches ? e.touches[0].clientY : e.clientY);
                                offsetY = y - _startDragY;
                                offsetX = x - _startDrag;

                                if (Math.abs(offsetY) < 100 && Math.abs(offsetX) >= 30) {
                                    _touchJump((offsetX < 0 ? 'next' : 'prev'));
                                    _startDrag = x;
                                    e.preventDefault();
                                }

                            }
                        }, 100),

                        'touchend.flipster touchcancel.flipster ': function () { _startDrag = false; }
                    });
                }
            }

            function init() {

                var style;

                self.css('visibility', 'hidden');

                index();

                if (_items.length <= 1) {
                    self.css('visibility', '');
                    return;
                }

                style = (settings.style ? 'flipster--' + settings.style.split(' ').join(' flipster--') : false);

                self.addClass([
                    classes.main,
                    (transformSupport ? 'flipster--transform' : ' flipster--no-transform'),
                    style, // 'flipster--'+settings.style : '' ),
                    (settings.click ? 'flipster--click' : '')
                ].join(' '));

                // Set the starting item
                if (settings.start) {
                    // Find the middle item if start = center
                    _currentIndex = (settings.start === 'center' ? Math.floor(_items.length / 2) : settings.start);
                }

                jump(_currentIndex);

                var images = self.find('img');

                if (images.length) {
                    var imagesLoaded = 0;

                    // Resize after all images have loaded.
                    images.on('load', function () {
                        imagesLoaded++;
                        if (imagesLoaded >= images.length) { show(); }
                    });

                    // Fallback to show Flipster while images load in case it takes a while.
                    setTimeout(show, 750);
                } else {
                    show();
                }

                // Attach event bindings.
                $window.on('resize.flipster', throttle(resize, 400));

                if (settings.autoplay) { play(); }

                if (settings.pauseOnHover) {
                    _container
                        .on('mouseenter.flipster', pause)
                        .on('mouseleave.flipster', function () {
                            if (_playing === -1) { play(); }
                        });
                }

                keyboardEvents(self);
                wheelEvents(_container);
                touchEvents(_container);
            }

            // public methods
            methods = {
                jump: jump,
                next: function () { return jump('next'); },
                prev: function () { return jump('prev'); },
                play: play,
                pause: pause,
                index: index
            };
            self.data('methods', methods);

            // Initialize if flipster is not already active.
            if (!self.hasClass(classes.active)) { init(); }
        });
    };
})(jQuery, window);



var flipContainer = $('.flipster'),
    flipItemContainer = flipContainer.find('.flip-items'),
    flipItem = flipContainer.find('li');

flipContainer.flipster({
    itemContainer: flipItemContainer,
    itemSelector: flipItem,
    loop: 2,
    start: 2,
    style: 'infinite-carousel',
    spacing: 10,
    scrollwheel: false,
    //nav: 'after',
    buttons: false
});

