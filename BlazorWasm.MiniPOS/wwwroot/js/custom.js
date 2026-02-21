window.getLocalStorageName = function () {
    return window.localStorage ? window.localStorage.name : null;
};

window.indexedDbSet = async function (key, value) {
    const db = await openDb();
    const tx = db.transaction("items", "readwrite");
    const store = tx.objectStore("items");
    await store.put({ id: key, data: value });
    return true;
};

window.indexedDbGet = async function (key) {
    const db = await openDb();
    const tx = db.transaction("items", "readonly");
    const store = tx.objectStore("items");
    const request = store.get(key);
    return new Promise((resolve, reject) => {
        request.onsuccess = () => resolve(request.result ? request.result.data : null);
        request.onerror = () => reject(request.error);
    });
};

window.indexedDbRemove = async function (key) {
    const db = await openDb();
    const tx = db.transaction("items", "readwrite");
    const store = tx.objectStore("items");
    await store.delete(key);
    return true;
};

window.indexedDbClear = async function () {
    const db = await openDb();
    const tx = db.transaction("items", "readwrite");
    const store = tx.objectStore("items");
    await store.clear();
    return true;
};

async function openDb() {
    return new Promise((resolve, reject) => {
        const request = indexedDB.open("MiniPosDb", 1);
        request.onupgradeneeded = () => {
            const db = request.result;
            if (!db.objectStoreNames.contains("items")) {
                db.createObjectStore("items", { keyPath: "id" });
            }
        };
        request.onsuccess = () => resolve(request.result);
        request.onerror = () => reject(request.error);
    });
}

window.applyHighchartsTheme = function () {
    const hc = window.Highcharts;
    if (!hc) return false;

    const root = document.documentElement;
    const themeKey = root.classList.contains("dark") ? "dark" : "light";
    if (window.__miniPosHighchartsThemeKey === themeKey) return true;

    const getCssVar = (name) => {
        try {
            return getComputedStyle(root).getPropertyValue(name).trim();
        } catch {
            return "";
        }
    };

    const toHsl = (raw, alpha) => {
        if (!raw) return "";
        const parts = raw.split(/\s+/).filter(Boolean);
        if (parts.length < 3) return "";
        const [h, s, l] = parts;
        return alpha === undefined
            ? `hsl(${h}, ${s}, ${l})`
            : `hsla(${h}, ${s}, ${l}, ${alpha})`;
    };

    const palette = [];
    for (let i = 1; i <= 9; i++) {
        const v = getCssVar(`--chart-${i}`);
        if (v) palette.push(v);
    }

    const colors = palette.length
        ? palette
        : ['#f97316', '#fb923c', '#fdba74', '#fed7aa', '#ffedd5', '#ea580c', '#c2410c', '#9a3412', '#7c2d12'];

    const foreground = toHsl(getCssVar("--foreground"));
    const mutedForeground = toHsl(getCssVar("--muted-foreground"));
    const border = toHsl(getCssVar("--border"));
    const grid = toHsl(getCssVar("--border"), 0.45);

    hc.theme = {
        colors,
        chart: {
            backgroundColor: "rgba(0, 0, 0, 0)",
            style: { fontFamily: "Outfit, sans-serif" }
        },
        title: {
            style: { color: foreground || "#1f2937", fontWeight: "bold" }
        },
        subtitle: {
            style: { color: mutedForeground || "#6b7280" }
        },
        xAxis: {
            lineColor: border || "#e5e7eb",
            tickColor: border || "#e5e7eb",
            gridLineColor: grid || "#f3f4f6",
            labels: { style: { color: mutedForeground || "#6b7280" } }
        },
        yAxis: {
            gridLineColor: grid || "#f3f4f6",
            labels: { style: { color: mutedForeground || "#6b7280" } },
            title: { style: { color: mutedForeground || "#6b7280" } }
        },
        legend: {
            itemStyle: { color: mutedForeground || "#4b5563" },
            itemHoverStyle: { color: foreground || "#1f2937" }
        },
        credits: { enabled: false }
    };

    hc.setOptions(hc.theme);
    window.__miniPosHighchartsThemeKey = themeKey;
    return true;
};

window.setMiniPosTheme = function (theme) {
    try {
        const root = document.documentElement;
        const isDark = theme === "dark";
        root.classList.toggle("dark", isDark);
        localStorage.setItem("minipos-theme", isDark ? "dark" : "light");

        // Update Highcharts (new + existing charts)
        if (window.Highcharts) {
            window.applyHighchartsTheme();
            const t = window.Highcharts.theme || {};
            const charts = window.Highcharts.charts || [];
            charts.forEach((c) => {
                if (!c) return;
                c.update({
                    colors: t.colors,
                    chart: t.chart,
                    title: t.title,
                    subtitle: t.subtitle,
                    xAxis: t.xAxis,
                    yAxis: t.yAxis,
                    legend: t.legend
                }, true, true, false);
            });
        }
    } catch {
        // ignore
    }
};

window.toggleMiniPosTheme = function () {
    try {
        const root = document.documentElement;
        const isDark = root.classList.contains("dark");
        window.setMiniPosTheme(isDark ? "light" : "dark");
    } catch {
        // ignore
    }
};

window.renderHighchartsColumn = function (containerId, title, categories, seriesData) {
    const hc = window.Highcharts;
    if (!hc) {
        console.error("Highcharts is not defined. Ensure Highcharts scripts are loaded before calling renderHighchartsColumn.");
        return;
    }
    window.applyHighchartsTheme();
    hc.chart(containerId, {
        chart: {
            type: 'column',
            borderRadius: 12
        },
        title: {
            text: title
        },
        xAxis: {
            categories: categories,
            crosshair: true
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Quantity'
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.0f}</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0,
                borderRadius: 4
            }
        },
        series: seriesData
    });
};

window.renderHighchartsPie = function (containerId, title, seriesData) {
    const hc = window.Highcharts;
    if (!hc) {
        console.error("Highcharts is not defined. Ensure Highcharts scripts are loaded before calling renderHighchartsPie.");
        return;
    }
    window.applyHighchartsTheme();
    hc.chart(containerId, {
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
        },
        title: {
            text: title
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        accessibility: {
            point: {
                valueSuffix: '%'
            }
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                },
                showInLegend: true
            }
        },
        series: [{
            name: 'Share',
            colorByPoint: true,
            data: seriesData
        }]
    });
};

window.renderHighchartsLine = function (containerId, title, categories, seriesData) {
    const hc = window.Highcharts;
    if (!hc) {
        console.error("Highcharts is not defined. Ensure Highcharts scripts are loaded before calling renderHighchartsLine.");
        return;
    }
    window.applyHighchartsTheme();
    hc.chart(containerId, {
        chart: {
            type: 'line',
            borderRadius: 12
        },
        title: {
            text: title
        },
        xAxis: {
            categories: categories
        },
        yAxis: {
            title: {
                text: 'Amount'
            }
        },
        plotOptions: {
            line: {
                dataLabels: {
                    enabled: true
                },
                enableMouseTracking: true
            }
        },
        series: seriesData
    });
};

window.renderHighchartsFunnel = function (containerId, title, seriesData) {
    const hc = window.Highcharts;
    if (!hc) {
        console.error("Highcharts is not defined. Ensure Highcharts scripts are loaded before calling renderHighchartsFunnel.");
        return;
    }
    window.applyHighchartsTheme();
    hc.chart(containerId, {
        chart: {
            type: 'funnel',
            borderRadius: 12
        },
        title: {
            text: title
        },
        plotOptions: {
            series: {
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b> ({point.y:,.0f})',
                    softConnector: true
                },
                center: ['40%', '50%'],
                neckWidth: '30%',
                neckHeight: '25%',
                width: '80%'
            }
        },
        legend: {
            enabled: false
        },
        series: [{
            name: 'Unique users',
            data: seriesData
        }]
    });
};
