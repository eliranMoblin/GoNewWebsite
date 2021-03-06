﻿/*!
* vue-carousel-3d v0.2.0
* (c) 2019 Vladimir Bujanovic
* https://github.com/wlada/vue-carousel-3d#readme
*/
! function(t, e) {
    "object" == typeof exports && "object" == typeof module ? module.exports = e() : "function" == typeof define && define.amd ? define([], e) : "object" == typeof exports ? exports.Carousel3d = e() : t.Carousel3d = e()
}(this, function() {
    return function(t) {
        function e(r) {
            if (n[r]) return n[r].exports;
            var i = n[r] = {
                exports: {},
                id: r,
                loaded: !1
            };
            return t[r].call(i.exports, i, i.exports, e), i.loaded = !0, i.exports
        }
        var n = {};
        return e.m = t, e.c = n, e.p = "", e(0)
    }([function(t, e, n) {
        "use strict";

        function r(t) {
            return t && t.__esModule ? t : {
                default: t
            }
        }
        Object.defineProperty(e, "__esModule", {
            value: !0
        }), e.Slide = e.Carousel3d = void 0;
        var i = n(1),
            o = r(i),
            s = n(16),
            a = r(s),
            u = function(t) {
                t.component("carousel3d", o.default), t.component("slide", a.default)
            };
        e.default = {
            install: u
        }, e.Carousel3d = o.default, e.Slide = a.default
    }, function(t, e, n) {
        n(2);
        var r = n(7)(n(8), n(63), "data-v-c06c963c", null);
        t.exports = r.exports
    }, function(t, e, n) {
        var r = n(3);
        "string" == typeof r && (r = [
            [t.id, r, ""]
        ]), r.locals && (t.exports = r.locals);
        n(5)("e749a8c4", r, !0)
    }, function(t, e, n) {
        e = t.exports = n(4)(), e.push([t.id, ".carousel-3d-container[data-v-c06c963c]{min-height:1px;width:100%;position:relative;z-index:0;overflow:hidden;margin:20px auto;box-sizing:border-box}.carousel-3d-slider[data-v-c06c963c]{position:relative;margin:0 auto;transform-style:preserve-3d;-webkit-perspective:1000px;-moz-perspective:1000px;perspective:1000px}", ""])
    }, function(t, e) {
        t.exports = function() {
            var t = [];
            return t.toString = function() {
                for (var t = [], e = 0; e < this.length; e++) {
                    var n = this[e];
                    n[2] ? t.push("@media " + n[2] + "{" + n[1] + "}") : t.push(n[1])
                }
                return t.join("")
            }, t.i = function(e, n) {
                "string" == typeof e && (e = [
                    [null, e, ""]
                ]);
                for (var r = {}, i = 0; i < this.length; i++) {
                    var o = this[i][0];
                    "number" == typeof o && (r[o] = !0)
                }
                for (i = 0; i < e.length; i++) {
                    var s = e[i];
                    "number" == typeof s[0] && r[s[0]] || (n && !s[2] ? s[2] = n : n && (s[2] = "(" + s[2] + ") and (" + n + ")"), t.push(s))
                }
            }, t
        }
    }, function(t, e, n) {
        function r(t) {
            for (var e = 0; e < t.length; e++) {
                var n = t[e],
                    r = c[n.id];
                if (r) {
                    r.refs++;
                    for (var i = 0; i < r.parts.length; i++) r.parts[i](n.parts[i]);
                    for (; i < n.parts.length; i++) r.parts.push(o(n.parts[i]));
                    r.parts.length > n.parts.length && (r.parts.length = n.parts.length)
                } else {
                    for (var s = [], i = 0; i < n.parts.length; i++) s.push(o(n.parts[i]));
                    c[n.id] = {
                        id: n.id,
                        refs: 1,
                        parts: s
                    }
                }
            }
        }

        function i() {
            var t = document.createElement("style");
            return t.type = "text/css", d.appendChild(t), t
        }

        function o(t) {
            var e, n, r = document.querySelector('style[data-vue-ssr-id~="' + t.id + '"]');
            if (r) {
                if (p) return v;
                r.parentNode.removeChild(r)
            }
            if (x) {
                var o = f++;
                r = h || (h = i()), e = s.bind(null, r, o, !1), n = s.bind(null, r, o, !0)
            } else r = i(), e = a.bind(null, r), n = function() {
                r.parentNode.removeChild(r)
            };
            return e(t),
                function(r) {
                    if (r) {
                        if (r.css === t.css && r.media === t.media && r.sourceMap === t.sourceMap) return;
                        e(t = r)
                    } else n()
                }
        }

        function s(t, e, n, r) {
            var i = n ? "" : r.css;
            if (t.styleSheet) t.styleSheet.cssText = m(e, i);
            else {
                var o = document.createTextNode(i),
                    s = t.childNodes;
                s[e] && t.removeChild(s[e]), s.length ? t.insertBefore(o, s[e]) : t.appendChild(o)
            }
        }

        function a(t, e) {
            var n = e.css,
                r = e.media,
                i = e.sourceMap;
            if (r && t.setAttribute("media", r), i && (n += "\n/*# sourceURL=" + i.sources[0] + " */", n += "\n/*# sourceMappingURL=data:application/json;base64," + btoa(unescape(encodeURIComponent(JSON.stringify(i)))) + " */"), t.styleSheet) t.styleSheet.cssText = n;
            else {
                for (; t.firstChild;) t.removeChild(t.firstChild);
                t.appendChild(document.createTextNode(n))
            }
        }
        var u = "undefined" != typeof document,
            l = n(6),
            c = {},
            d = u && (document.head || document.getElementsByTagName("head")[0]),
            h = null,
            f = 0,
            p = !1,
            v = function() {},
            x = "undefined" != typeof navigator && /msie [6-9]\b/.test(navigator.userAgent.toLowerCase());
        t.exports = function(t, e, n) {
            p = n;
            var i = l(t, e);
            return r(i),
                function(e) {
                    for (var n = [], o = 0; o < i.length; o++) {
                        var s = i[o],
                            a = c[s.id];
                        a.refs--, n.push(a)
                    }
                    e ? (i = l(t, e), r(i)) : i = [];
                    for (var o = 0; o < n.length; o++) {
                        var a = n[o];
                        if (0 === a.refs) {
                            for (var u = 0; u < a.parts.length; u++) a.parts[u]();
                            delete c[a.id]
                        }
                    }
                }
        };
        var m = function() {
            var t = [];
            return function(e, n) {
                return t[e] = n, t.filter(Boolean).join("\n")
            }
        }()
    }, function(t, e) {
        t.exports = function(t, e) {
            for (var n = [], r = {}, i = 0; i < e.length; i++) {
                var o = e[i],
                    s = o[0],
                    a = o[1],
                    u = o[2],
                    l = o[3],
                    c = {
                        id: t + ":" + i,
                        css: a,
                        media: u,
                        sourceMap: l
                    };
                r[s] ? r[s].parts.push(c) : n.push(r[s] = {
                    id: s,
                    parts: [c]
                })
            }
            return n
        }
    }, function(t, e) {
        t.exports = function(t, e, n, r) {
            var i, o = t = t || {},
                s = typeof t.default;
            "object" !== s && "function" !== s || (i = t, o = t.default);
            var a = "function" == typeof o ? o.options : o;
            if (e && (a.render = e.render, a.staticRenderFns = e.staticRenderFns), n && (a._scopeId = n), r) {
                var u = a.computed || (a.computed = {});
                Object.keys(r).forEach(function(t) {
                    var e = r[t];
                   u[t] = function() {
                        return e
                    }
                })
            }
            return {
                esModule: i,
                exports: o,
                options: a
            }
        }
    }, function(t, e, n) {
        (function(t) {
            "use strict";

            function r(t) {
                return t && t.__esModule ? t : {
                    default: t
                }
            }
            Object.defineProperty(e, "__esModule", {
                value: !0
            });
            var i = n(10),
                o = r(i),
                s = n(11),
                a = r(s),
                u = n(16),
                l = r(u),
                c = function() {};
            e.default = {
                name: "carousel3d",
                components: {
                    Controls: a.default,
                    Slide: l.default
                },
                props: {
                    count: {
                        type: [Number, String],
                        default: 0
                    },
                    perspective: {
                        type: [Number, String],
                        default: 35
                    },
                    display: {
                        type: [Number, String],
                        default: 5
                    },
                    loop: {
                        type: Boolean,
                        default: !0
                    },
                    animationSpeed: {
                        type: [Number, String],
                        default: 500
                    },
                    dir: {
                        type: String,
                        default: "rtl"
                    },
                    width: {
                        type: [Number, String],
                        default: 360
                    },
                    height: {
                        type: [Number, String],
                        default: 270
                    },
                    border: {
                        type: [Number, String],
                        default: 1
                    },
                   space: {
                        type: [Number, String],
                        default: "auto"
                    },
                    startIndex: {
                        type: [Number, String],
                        default: 0
                    },
                    clickable: {
                        type: Boolean,
                        default: !0
                    },
                    disable3d: {
                        type: Boolean,
                        default: !1
                    },
                    minSwipeDistance: {
                        type: Number,
                        default: 10
                    },
                    inverseScaling: {
                        type: [Number, String],
                        default: 300
                    },
                    controlsVisible: {
                        type: Boolean,
                        default: !1
                    },
                    controlsPrevHtml: {
                        type: String,
                        default: "&lsaquo;"
                    },
                    controlsNextHtml: {
                        type: String,
                        default: "&rsaquo;"
                    },
                    controlsWidth: {
                        type: [String, Number],
                        default: 50
                    },
                    controlsHeight: {
                        type: [String, Number],
                        default: 50
                    },
                    onLastSlide: {
                        type: Function,
                        default: c
                   },
                    onSlideChange: {
                        type: Function,
                        default: c
                    },
                    bias: {
                        type: String,
                        default: "left"
                    },
                    onMainSlideClick: {
                        type: Function,
                        default: c
                    }
                },
                data: function() {
                    return {
                        viewport: 0,
                        currentIndex: 0,
                        total: 0,
                        dragOffset: 0,
                        dragStartX: 0,
                        mousedown: !1,
                        zIndex: 998
                    }
                },
                mixins: [o.default],
                watch: {
                    count: function() {
                        this.computeData()
                    }
                },
                computed: {
                    isLastSlide: function() {
                        return this.currentIndex === this.total - 1
                    },
                    isFirstSlide: function() {
                        return 0 === this.currentIndex
                    },
                    isNextPossible: function() {
                        return !(!this.loop && this.isLastSlide)
                    },
                    isPrevPossible: function() {
                        return !(!this.loop && this.isFirstSlide)
                    },
                    slideWidth: function() {
                        var e = this.viewport,
                            n = parseInt(this.width) + 2 * parseInt(this.border, 10);
                        return e < n && t.browser ? e : n
                    },
                    slideHeight: function() {
                        var t = parseInt(this.width, 10) + 2 * parseInt(this.border, 10),
                            e = parseInt(parseInt(this.height) + 2 * this.border, 10),
                            n = this.calculateAspectRatio(t, e);
                        return this.slideWidth / n
                    },
                    visible: function() {
                        var t = this.display > this.total ? this.total : this.display;
                        return t
                    },
                    hasHiddenSlides: function() {
                        return this.total > this.visible
                    },
                    leftIndices: function() {
                        var t = (this.visible - 1) / 2;
                        t = "left" === this.bias.toLowerCase() ? Math.ceil(t) : Math.floor(t);
                        for (var e = [], n = 1; n <= t; n++) e.push("ltr" === this.dir ? (this.currentIndex + n) % this.total : (this.currentIndex - n) % this.total);
                        return e
                    },
                    rightIndices: function() {
                        var t = (this.visible - 1) / 2;
                        t = "right" === this.bias.toLowerCase() ? Math.ceil(t) : Math.floor(t);
                        for (var e = [], n = 1; n <= t; n++) e.push("ltr" === this.dir ? (this.currentIndex - n) % this.total : (this.currentIndex + n) % this.total);
                        return e
                    },
                    leftOutIndex: function() {
                        var t = (this.visible - 1) / 2;
                        return t = "left" === this.bias.toLowerCase() ? Math.ceil(t) : Math.floor(t), t++, "ltr" === this.dir ? this.total - this.currentIndex - t <= 0 ? -parseInt(this.total - this.currentIndex - t) : this.currentIndex + t : this.currentIndex - t
                    },
                    rightOutIndex: function() {
                        var t = (this.visible - 1) / 2;
                        return t = "right" === this.bias.toLowerCase() ? Math.ceil(t) : Math.floor(t), t++, "ltr" === this.dir ? this.currentIndex - t : this.total - this.currentIndex - t <= 0 ? -parseInt(this.total - this.currentIndex - t, 10) : this.currentIndex + t
                    }
                },
                methods: {
                    goNext: function() {
                        this.isNextPossible && (this.isLastSlide ? this.goSlide(0) : this.goSlide(this.currentIndex + 1))
                    },
                    goPrev: function() {
                        this.isPrevPossible && (this.isFirstSlide ? this.goSlide(this.total - 1) : this.goSlide(this.currentIndex - 1))
                    },
                    goSlide: function(t) {
                        var e = this;
                        this.currentIndex = t < 0 || t > this.total - 1 ? 0 : t, this.isLastSlide && (this.onLastSlide !== c && console.warn("onLastSlide deprecated, please use @last-slide"), this.onLastSlide(this.currentIndex), this.$emit("last-slide", this.currentIndex)), this.$emit("before-slide-change", this.currentIndex), setTimeout(function() {
                            return e.animationEnd()
                        }, this.animationSpeed)
                    },
                    goFar: function(t) {
                        var e = this,
                            n = t === this.total - 1 && this.isFirstSlide ? -1 : t - this.currentIndex;
                        this.isLastSlide && 0 === t && (n = 1);
                        for (var r = n < 0 ? -n : n, i = 0, o = 0; o < r;) {
                            o += 1;
                            var s = 1 === r ? 0 : i;
                            setTimeout(function() {
                                return n < 0 ? e.goPrev(r) : e.goNext(r)
                            }, s), i += this.animationSpeed / r
                        }
                    },
                    animationEnd: function() {
                        this.onSlideChange !== c && console.warn("onSlideChange deprecated, please use @after-slide-change"), this.onSlideChange(this.currentIndex), this.$emit("after-slide-change", this.currentIndex)
                    },
                    handleMouseup: function() {
                        this.mousedown = !1, this.dragOffset = 0
                    },
                    handleMousedown: function(t) {
                        t.touches || t.preventDefault(), this.mousedown = !0, this.dragStartX = "ontouchstart" in window ? t.touches[0].clientX : t.clientX
                    },
                    handleMousemove: function(t) {
                        if (this.mousedown) {
                            var e = "ontouchstart" in window ? t.touches[0].clientX : t.clientX,
                                n = this.dragStartX - e;
                            this.dragOffset = n, this.dragOffset > this.minSwipeDistance ? (this.handleMouseup(), this.goNext()) : this.dragOffset < -this.minSwipeDistance && (this.handleMouseup(), this.goPrev())
                        }
                    },
                    attachMutationObserver: function() {
                        var t = this,
                            e = window.MutationObserver || window.WebKitMutationObserver || window.MozMutationObserver;
                        if (e) {
                            var n = {
                                attributes: !0,
                                childList: !0,
                                characterData: !0
                            };
                            this.mutationObserver = new e(function() {
                                t.$nextTick(function() {
                                    t.computeData()
                                })
                            }), this.$el && this.mutationObserver.observe(this.$el, n)
                        }
                    },
                    detachMutationObserver: function() {
                        this.mutationObserver && this.mutationObserver.disconnect()
                    },
                    getSlideCount: function() {
                        return void 0 !== this.$slots.default ? this.$slots.default.filter(function(t) {
                            return void 0 !== t.tag
                        }).length : 0
                    },
                    calculateAspectRatio: function(t, e) {
                        return Math.min(t / e)
                    },
                    computeData: function(t) {
                        this.total = this.getSlideCount(), (t || this.currentIndex >= this.total) && (this.currentIndex = parseInt(this.startIndex) > this.total - 1 ? this.total - 1 : parseInt(this.startIndex)), this.viewport = this.$el.clientWidth
                    },
                    setSize: function() {
                        this.$el.style.cssText += "height:" + this.slideHeight + "px;", this.$el.childNodes[0].style.cssText += "width:" + this.slideWidth + "px; height:" + this.slideHeight + "px;"
                    }
                },
                mounted: function() {
                    this.computeData(!0), this.attachMutationObserver(), this.$isServer || (window.addEventListener("resize", this.setSize), "ontouchstart" in window ? (this.$el.addEventListener("touchstart", this.handleMousedown), this.$el.addEventListener("touchend", this.handleMouseup), this.$el.addEventListener("touchmove", this.handleMousemove)) : (this.$el.addEventListener("mousedown", this.handleMousedown), this.$el.addEventListener("mouseup", this.handleMouseup), this.$el.addEventListener("mousemove", this.handleMousemove)))
                },
                beforeDestroy: function() {
                    this.$isServer || (this.detachMutationObserver(), "ontouchstart" in window ? this.$el.removeEventListener("touchmove", this.handleMousemove) : this.$el.removeEventListener("mousemove", this.handleMousemove), window.removeEventListener("resize", this.setSize))
                }
            }
        }).call(e, n(9))
    }, function(t, e) {
        function n() {
            throw new Error("setTimeout has not been defined")
        }

        function r() {
            throw new Error("clearTimeout has not been defined")
        }

        function i(t) {
            if (c === setTimeout) return setTimeout(t, 0);
            if ((c === n || !c) && setTimeout) return c = setTimeout, setTimeout(t, 0);
            try {
                return c(t, 0)
            } catch (e) {
                try {
                    return c.call(null, t, 0)
                } catch (e) {
                    return c.call(this, t, 0)
                }
            }
        }

        function o(t) {
            if (d === clearTimeout) return clearTimeout(t);
            if ((d === r || !d) && clearTimeout) return d = clearTimeout, clearTimeout(t);
            try {
                return d(t)
            } catch (e) {
                try {
                    return d.call(null, t)
                } catch (e) {
                    return d.call(this, t)
                }
            }
        }

        function s() {
            v && f && (v = !1, f.length ? p = f.concat(p) : x = -1, p.length && a())
        }

        function a() {
            if (!v) {
                var t = i(s);
                v = !0;
                for (var e = p.length; e;) {
                    for (f = p, p = []; ++x < e;) f && f[x].run();
                    x = -1, e = p.length
                }
                f = null, v = !1, o(t)
            }
        }

        function u(t, e) {
            this.fun = t, this.array = e
        }

        function l() {}
        var c, d, h = t.exports = {};
        ! function() {
            try {
                c = "function" == typeof setTimeout ? setTimeout : n
            } catch (t) {
                c = n
            }
            try {
                d = "function" == typeof clearTimeout ? clearTimeout : r
            } catch (t) {
                d = r
            }
        }();
        var f, p = [],
            v = !1,
            x = -1;
        h.nextTick = function(t) {
            var e = new Array(arguments.length - 1);
            if (arguments.length > 1)
                for (var n = 1; n < arguments.length; n++) e[n - 1] = arguments[n];
            p.push(new u(t, e)), 1 !== p.length || v || i(a)
        }, u.prototype.run = function() {
            this.fun.apply(null, this.array)
        }, h.title = "browser", h.browser = !0, h.env = {}, h.argv = [], h.version = "", h.versions = {}, h.on = l, h.addListener = l, h.once = l, h.off = l, h.removeListener = l, h.removeAllListeners = l, h.emit = l, h.prependListener = l, h.prependOnceListener = l, h.listeners = function(t) {
            return []
        }, h.binding = function(t) {
            throw new Error("process.binding is not supported")
        }, h.cwd = function() {
            return "/"
        }, h.chdir = function(t) {
            throw new Error("process.chdir is not supported")
        }, h.umask = function() {
            return 0
        }
    }, function(t, e) {
        "use strict";
        Object.defineProperty(e, "__esModule", {
            value: !0
        });
        var n = {
            props: {
                autoplay: {
                    type: Boolean,
                    default: !1
                },
                autoplayTimeout: {
                    type: Number,
                    default: 2e3
                },
                autoplayHoverPause: {
                    type: Boolean,
                    default: !0
                }
            },
            data: function() {
                return {
                    autoplayInterval: null
                }
            },
            destroyed: function() {
                this.pauseAutoplay(), this.$isServer || (this.$el.removeEventListener("mouseenter", this.pauseAutoplay), this.$el.removeEventListener("mouseleave", this.startAutoplay))
            },
            methods: {
                pauseAutoplay: function() {
                    this.autoplayInterval && (this.autoplayInterval = clearInterval(this.autoplayInterval))
                },
                startAutoplay: function() {
                    var t = this;
                    this.autoplay && (this.autoplayInterval = setInterval(function() {
                        "ltr" === t.dir ? t.goPrev() : t.goNext()
                    }, this.autoplayTimeout))
                }
            },
            mounted: function() {
                !this.$isServer && this.autoplayHoverPause && (this.$el.addEventListener("mouseenter", this.pauseAutoplay), this.$el.addEventListener("mouseleave", this.startAutoplay)), this.startAutoplay()
            }
        };
        e.default = n
    }, function(t, e, n) {
        n(12);
        var r = n(7)(n(14), n(15), "data-v-43e93932", null);
        t.exports = r.exports
    }, function(t, e, n) {
        var r = n(13);
        "string" == typeof r && (r = [
            [t.id, r, ""]
        ]), r.locals && (t.exports = r.locals);
        n(5)("06c66230", r, !0)
    }, function(t, e, n) {
        e = t.exports = n(4)(), e.push([t.id, ".carousel-3d-controls[data-v-43e93932]{position:absolute;top:50%;height:0;margin-top:-30px;left:0;width:100%;z-index:1000}.next[data-v-43e93932],.prev[data-v-43e93932]{width:60px;position:absolute;z-index:1010;font-size:60px;height:60px;line-height:60px;color:#333;-webkit-user-select:none;-moz-user-select:none;-ms-user-select:none;user-select:none;text-decoration:none;top:0}.next[data-v-43e93932]:hover,.prev[data-v-43e93932]:hover{cursor:pointer;opacity:.7}.prev[data-v-43e93932]{left:10px;text-align:left}.next[data-v-43e93932]{right:10px;text-align:right}.disabled[data-v-43e93932],.disabled[data-v-43e93932]:hover{opacity:.2;cursor:default}", ""])
    }, function(t, e) {
        "use strict";
        Object.defineProperty(e, "__esModule", {
            value: !0
        }), e.default = {
            name: "controls",
            props: {
                width: {
                    type: [String, Number],
                    default: 50
                },
                height: {
                    type: [String, Number],
                    default: 60
                },
                prevHtml: {
                    type: String,
                    default: "&lsaquo;"
                },
                nextHtml: {
                    type: String,
                    default: "&rsaquo;"
                }
            },
            data: function() {
                return {
                    parent: this.$parent
                }
            }
        }
    }, function(t, e) {
        t.exports = {
            render: function() {
                var t = this,
                    e = t.$createElement,
                    n = t._self._c || e;
                return n("div", {
                    staticClass: "carousel-3d-controls"
                }, [n("a", {
                    staticClass: "prev",
                    class: {
                        disabled: !t.parent.isPrevPossible
                    },
                    style: "width: " + t.width + "px; height: " + t.height + "px; line-height: " + t.height + "px;",
                    attrs: {
                        href: "#"
                    },
                    on: {
                        click: function(e) {
                            return e.preventDefault(), t.parent.goPrev()
                        }
                    }
                }, [n("span", {
                    domProps: {
                        innerHTML: t._s(t.prevHtml)
                    }
                })]), t._v(" "), n("a", {
                    staticClass: "next",
                    class: {
                        disabled: !t.parent.isNextPossible
                    },
                    style: "width: " + t.width + "px; height: " + t.height + "px; line-height: " + t.height + "px;",
                    attrs: {
                        href: "#"
                    },
                    on: {
                        click: function(e) {
                            return e.preventDefault(), t.parent.goNext()
                        }
                    }
                }, [n("span", {
                    domProps: {
                        innerHTML: t._s(t.nextHtml)
                    }
                })])])
            },
            staticRenderFns: []
        }
    }, function(t, e, n) {
        n(17);
        var r = n(7)(n(19), n(62), null, null);
        t.exports = r.exports
    }, function(t, e, n) {
        var r = n(18);
        "string" == typeof r && (r = [
            [t.id, r, ""]
        ]), r.locals && (t.exports = r.locals);
        n(5)("1dbacf8a", r, !0)
    }, function(t, e, n) {
        e = t.exports = n(4)(), e.push([t.id, ".carousel-3d-slide{position:absolute;opacity:0;visibility:hidden;overflow:hidden;top:0;border-radius:1px;border-color:#000;border-color:rgba(0,0,0,.4);border-style:solid;background-size:cover;background-color:#ccc;display:block;margin:0;box-sizing:border-box;text-align:left}.carousel-3d-slide img{width:100%}.carousel-3d-slide.current{opacity:1!important;visibility:visible!important;transform:none!important;z-index:999}", ""])
    }, function(t, e, n) {
        "use strict";

        function r(t) {
            return t && t.__esModule ? t : {
                default: t
            }
        }
        Object.defineProperty(e, "__esModule", {
            value: !0
        });
        var i = n(20),
            o = r(i),
            s = n(40),
            a = r(s);
        e.default = {
            name: "slide",
            props: {
                index: {
                    type: Number
                }
            },
            data: function() {
                return {
                    parent: this.$parent,
                    styles: {},
                    zIndex: 999
                }
            },
            computed: {
                isCurrent: function() {
                    return this.index === this.parent.currentIndex
                },
                leftIndex: function() {
                   return this.getSideIndex(this.parent.leftIndices)
                },
                rightIndex: function() {
                    return this.getSideIndex(this.parent.rightIndices)
                },
                slideStyle: function() {
                    var t = {};
                    if (!this.isCurrent) {
                        var e = this.leftIndex,
                            n = this.rightIndex;
                        (n >= 0 || e >= 0) && (t = n >= 0 ? this.calculatePosition(n, !0, this.zIndex) : this.calculatePosition(e, !1, this.zIndex), t.opacity = 1, t.visibility = "visible"), this.parent.hasHiddenSlides && (this.matchIndex(this.parent.leftOutIndex) ? t = this.calculatePosition(this.parent.leftIndices.length - 1, !1, this.zIndex) : this.matchIndex(this.parent.rightOutIndex) && (t = this.calculatePosition(this.parent.rightIndices.length - 1, !0, this.zIndex)))
                    }
                    return (0, a.default)(t, {
                        "border-width": this.parent.border + "px",
                        width: this.parent.slideWidth + "px",
                        height: this.parent.slideHeight + "px",
                        transition: " transform " + this.parent.animationSpeed + "ms,                opacity " + this.parent.animationSpeed + "ms,                visibility " + this.parent.animationSpeed + "ms"
                    })
                },
                computedClasses: function() {
                    var t;
                    return t = {}, (0, o.default)(t, "left-" + (this.leftIndex + 1), this.leftIndex >= 0), (0, o.default)(t, "right-" + (this.rightIndex + 1), this.rightIndex >= 0), (0, o.default)(t, "current", this.isCurrent), t
                }
            },
            methods: {
                getSideIndex: function(t) {
                    var e = this,
                        n = -1;
                    return t.forEach(function(t, r) {
                        e.matchIndex(t) && (n = r)
                    }), n
                },
                matchIndex: function(t) {
                    return t >= 0 ? this.index === t : this.parent.total + t === this.index
                },
                calculatePosition: function(t, e, n) {
                    var r = this.parent.disable3d ? 0 : parseInt(this.parent.inverseScaling) + 100 * (t + 1),
                        i = this.parent.disable3d ? 0 : parseInt(this.parent.perspective),
                        o = "auto" === this.parent.space ? parseInt((t + 1) * (this.parent.width / 1.5), 10) : parseInt((t + 1) * this.parent.space, 10),
                        s = e ? "translateX(" + o + "px) translateZ(-" + r + "px) rotateY(-" + i + "deg)" : "translateX(-" + o + "px) translateZ(-" + r + "px) rotateY(" + i + "deg)",
                        a = "auto" === this.parent.space ? 0 : parseInt((t + 1) * this.parent.space);
                    return {
                        transform: s,
                        top: a,
                        zIndex: n - (Math.abs(t) + 1)
                    }
                },
                goTo: function() {
                    this.isCurrent ? this.parent.onMainSlideClick() : this.parent.clickable === !0 && this.parent.goFar(this.index)
                }
            }
        }
    }, function(t, e, n) {
        "use strict";

        function r(t) {
            return t && t.__esModule ? t : {
                default: t
            }
        }
        e.__esModule = !0;
        var i = n(21),
            o = r(i);
        e.default = function(t, e, n) {
            return e in t ? (0, o.default)(t, e, {
                value: n,
                enumerable: !0,
                configurable: !0,
                writable: !0
            }) : t[e] = n, t
        }
    }, function(t, e, n) {
        t.exports = {
            default: n(22),
            __esModule: !0
        }
    }, function(t, e, n) {
        n(23);
        var r = n(26).Object;
        t.exports = function(t, e, n) {
            return r.defineProperty(t, e, n)
        }
    }, function(t, e, n) {
        var r = n(24);
        r(r.S + r.F * !n(34), "Object", {
            defineProperty: n(30).f
        })
    }, function(t, e, n) {
        var r = n(25),
            i = n(26),
            o = n(27),
            s = n(29),
            a = n(39),
            u = "prototype",
            l = function(t, e, n) {
                var c, d, h, f = t & l.F,
                    p = t & l.G,
                    v = t & l.S,
                    x = t & l.P,
                    m = t & l.B,
                    g = t & l.W,
                    y = p ? i : i[e] || (i[e] = {}),
                    b = y[u],
                    w = p ? r : v ? r[e] : (r[e] || {})[u];
                p && (n = e);
                for (c in n) d = !f && w && void 0 !== w[c], d && a(y, c) || (h = d ? w[c] : n[c], y[c] = p && "function" != typeof w[c] ? n[c] : m && d ? o(h, r) : g && w[c] == h ? function(t) {
                    var e = function(e, n, r) {
                        if (this instanceof t) {
                            switch (arguments.length) {
                                case 0:
                                    return new t;
                                case 1:
                                    return new t(e);
                                case 2:
                                    return new t(e, n)
                            }
                            return new t(e, n, r)
                        }
                        return t.apply(this, arguments)
                    };
                    return e[u] = t[u], e
                }(h) : x && "function" == typeof h ? o(Function.call, h) : h, x && ((y.virtual || (y.virtual = {}))[c] = h, t & l.R && b && !b[c] && s(b, c, h)))
            };
        l.F = 1, l.G = 2, l.S = 4, l.P = 8, l.B = 16, l.W = 32, l.U = 64, l.R = 128, t.exports = l
    }, function(t, e) {
        var n = t.exports = "undefined" != typeof window && window.Math == Math ? window : "undefined" != typeof self && self.Math == Math ? self : Function("return this")();
        "number" == typeof __g && (__g = n)
    }, function(t, e) {
        var n = t.exports = {
            version: "2.5.7"
        };
        "number" == typeof __e && (__e = n)
    }, function(t, e, n) {
        var r = n(28);
        t.exports = function(t, e, n) {
            if (r(t), void 0 === e) return t;
            switch (n) {
                case 1:
                    return function(n) {
                        return t.call(e, n)
                    };
                case 2:
                    return function(n, r) {
                        return t.call(e, n, r)
                    };
                case 3:
                    return function(n, r, i) {
                        return t.call(e, n, r, i)
                    }
            }
            return function() {
                return t.apply(e, arguments)
            }
        }
    }, function(t, e) {
        t.exports = function(t) {
            if ("function" != typeof t) throw TypeError(t + " is not a function!");
            return t
        }
    }, function(t, e, n) {
        var r = n(30),
            i = n(38);
        t.exports = n(34) ? function(t, e, n) {
            return r.f(t, e, i(1, n))
        } : function(t, e, n) {
            return t[e] = n, t
        }
    }, function(t, e, n) {
        var r = n(31),
            i = n(33),
            o = n(37),
           s = Object.defineProperty;
        e.f = n(34) ? Object.defineProperty : function(t, e, n) {
            if (r(t), e = o(e, !0), r(n), i) try {
                return s(t, e, n)
            } catch (t) {}
            if ("get" in n || "set" in n) throw TypeError("Accessors not supported!");
            return "value" in n && (t[e] = n.value), t
        }
    }, function(t, e, n) {
        var r = n(32);
        t.exports = function(t) {
            if (!r(t)) throw TypeError(t + " is not an object!");
            return t
        }
    }, function(t, e) {
        t.exports = function(t) {
            return "object" == typeof t ? null !== t : "function" == typeof t
        }
    }, function(t, e, n) {
        t.exports = !n(34) && !n(35)(function() {
            return 7 != Object.defineProperty(n(36)("div"), "a", {
                get: function() {
                    return 7
                }
            }).a
        })
    }, function(t, e, n) {
        t.exports = !n(35)(function() {
            return 7 != Object.defineProperty({}, "a", {
                get: function() {
                    return 7
                }
            }).a
        })
    }, function(t, e) {
        t.exports = function(t) {
            try {
                return !!t()
            } catch (t) {
                return !0
            }
        }
    }, function(t, e, n) {
        var r = n(32),
            i = n(25).document,
            o = r(i) && r(i.createElement);
        t.exports = function(t) {
            return o ? i.createElement(t) : {}
        }
    }, function(t, e, n) {
        var r = n(32);
        t.exports = function(t, e) {
            if (!r(t)) return t;
            var n, i;
            if (e && "function" == typeof(n = t.toString) && !r(i = n.call(t))) return i;
            if ("function" == typeof(n = t.valueOf) && !r(i = n.call(t))) return i;
            if (!e && "function" == typeof(n = t.toString) && !r(i = n.call(t))) return i;
            throw TypeError("Can't convert object to primitive value")
        }
    }, function(t, e) {
        t.exports = function(t, e) {
            return {
                enumerable: !(1 & t),
                configurable: !(2 & t),
                writable: !(4 & t),
                value: e
            }
        }
    }, function(t, e) {
        var n = {}.hasOwnProperty;
        t.exports = function(t, e) {
            return n.call(t, e)
        }
    }, function(t, e, n) {
        t.exports = {
            default: n(41),
            __esModule: !0
        }
    }, function(t, e, n) {
        n(42), t.exports = n(26).Object.assign
    }, function(t, e, n) {
        var r = n(24);
        r(r.S + r.F, "Object", {
            assign: n(43)
        })
    }, function(t, e, n) {
        "use strict";
        var r = n(44),
            i = n(59),
            o = n(60),
            s = n(61),
            a = n(47),
            u = Object.assign;
        t.exports = !u || n(35)(function() {
            var t = {},
                e = {},
                n = Symbol(),
                r = "abcdefghijklmnopqrst";
            return t[n] = 7, r.split("").forEach(function(t) {
                e[t] = t
            }), 7 != u({}, t)[n] || Object.keys(u({}, e)).join("") != r
        }) ? function(t, e) {
            for (var n = s(t), u = arguments.length, l = 1, c = i.f, d = o.f; u > l;)
                for (var h, f = a(arguments[l++]), p = c ? r(f).concat(c(f)) : r(f), v = p.length, x = 0; v > x;) d.call(f, h = p[x++]) && (n[h] = f[h]);
            return n
        } : u
    }, function(t, e, n) {
        var r = n(45),
            i = n(58);
        t.exports = Object.keys || function(t) {
            return r(t, i)
        }
    }, function(t, e, n) {
        var r = n(39),
            i = n(46),
            o = n(50)(!1),
            s = n(54)("IE_PROTO");
        t.exports = function(t, e) {
            var n, a = i(t),
                u = 0,
                l = [];
            for (n in a) n != s && r(a, n) && l.push(n);
            for (; e.length > u;) r(a, n = e[u++]) && (~o(l, n) || l.push(n));
            return l
        }
    }, function(t, e, n) {
        var r = n(47),
            i = n(49);
        t.exports = function(t) {
            return r(i(t))
        }
    }, function(t, e, n) {
        var r = n(48);
        t.exports = Object("z").propertyIsEnumerable(0) ? Object : function(t) {
            return "String" == r(t) ? t.split("") : Object(t)
        }
    }, function(t, e) {
        var n = {}.toString;
        t.exports = function(t) {
            return n.call(t).slice(8, -1)
        }
    }, function(t, e) {
        t.exports = function(t) {
            if (void 0 == t) throw TypeError("Can't call method on  " + t);
            return t
        }
    }, function(t, e, n) {
        var r = n(46),
            i = n(51),
            o = n(53);
        t.exports = function(t) {
            return function(e, n, s) {
                var a, u = r(e),
                    l = i(u.length),
                    c = o(s, l);
                if (t && n != n) {
                    for (; l > c;)
                        if (a = u[c++], a != a) return !0
                } else
                    for (; l > c; c++)
                        if ((t || c in u) && u[c] === n) return t || c || 0; return !t && -1
            }
        }
    }, function(t, e, n) {
        var r = n(52),
            i = Math.min;
        t.exports = function(t) {
            return t > 0 ? i(r(t), 9007199254740991) : 0
        }
    }, function(t, e) {
        var n = Math.ceil,
            r = Math.floor;
        t.exports = function(t) {
            return isNaN(t = +t) ? 0 : (t > 0 ? r : n)(t)
        }
    }, function(t, e, n) {
        var r = n(52),
            i = Math.max,
            o = Math.min;
        t.exports = function(t, e) {
            return t = r(t), t < 0 ? i(t + e, 0) : o(t, e)
        }
    }, function(t, e, n) {
        var r = n(55)("keys"),
            i = n(57);
        t.exports = function(t) {
            return r[t] || (r[t] = i(t))
        }
    }, function(t, e, n) {
        var r = n(26),
            i = n(25),
            o = "__core-js_shared__",
            s = i[o] || (i[o] = {});
        (t.exports = function(t, e) {
            return s[t] || (s[t] = void 0 !== e ? e : {})
        })("versions", []).push({
            version: r.version,
            mode: n(56) ? "pure" : "global",
            copyright: "© 2018 Denis Pushkarev (zloirock.ru)"
        })
    }, function(t, e) {
        t.exports = !0
    }, function(t, e) {
        var n = 0,
            r = Math.random();
        t.exports = function(t) {
            return "Symbol(".concat(void 0 === t ? "" : t, ")_", (++n + r).toString(36))
        }
    }, function(t, e) {
        t.exports = "constructor,hasOwnProperty,isPrototypeOf,propertyIsEnumerable,toLocaleString,toString,valueOf".split(",")
    }, function(t, e) {
        e.f = Object.getOwnPropertySymbols
    }, function(t, e) {
        e.f = {}.propertyIsEnumerable
    }, function(t, e, n) {
        var r = n(49);
        t.exports = function(t) {
            return Object(r(t))
        }
    }, function(t, e) {
        t.exports = {
            render: function() {
                var t = this,
                    e = t.$createElement,
                    n = t._self._c || e;
                return n("div", {
                    staticClass: "carousel-3d-slide",
                    class: t.computedClasses,
                    style: t.slideStyle,
                    on: {
                        click: function(e) {
                            return t.goTo()
                        }
                    }
                }, [t._t("default", null, {
                    index: t.index,
                    isCurrent: t.isCurrent,
                    leftIndex: t.leftIndex,
                    rightIndex: t.rightIndex
                })], 2)
            },
            staticRenderFns: []
        }
    }, function(t, e) {
        t.exports = {
            render: function() {
                var t = this,
                    e = t.$createElement,
                    n = t._self._c || e;
                return n("div", {
                    staticClass: "carousel-3d-container",
                    style: {
                        height: this.slideHeight + "px"
                    }
                }, [n("div", {
                    staticClass: "carousel-3d-slider",
                    style: {
                        width: this.slideWidth + "px",
                        height: this.slideHeight + "px"
                    }
                }, [t._t("default")], 2), t._v(" "), t.controlsVisible ? n("controls", {
                    attrs: {
                        "next-html": t.controlsNextHtml,
                        "prev-html": t.controlsPrevHtml,
                        width: t.controlsWidth,
                        height: t.controlsHeight
                    }
                }) : t._e()], 1)
            },
            staticRenderFns: []
        }
    }])
});



