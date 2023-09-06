window.bindPieChart = function (id, labels, series) {
    var options = {
        series: series,
        chart: {
            width: 380,
            type: 'pie',
        },
        labels: labels,
        responsive: [{
            breakpoint: 480,
            options: {
                chart: {
                    width: 200
                },
                legend: {
                    position: 'bottom'
                }
            }
        }]
    };

    var chart = new ApexCharts(document.querySelector("#" + id), options);
    chart.render();
}

window.bindPieChart2 = function (id, data) {
    console.log({ id, data });
    // data = [{
    //     name: 'Chrome',
    //     y: 70.67,
    //     sliced: true,
    //     selected: true
    // }, {
    //     name: 'Edge',
    //     y: 14.77
    // }, {
    //     name: 'Firefox',
    //     y: 4.86
    // }, {
    //     name: 'Safari',
    //     y: 2.63
    // }, {
    //     name: 'Internet Explorer',
    //     y: 1.53
    // }, {
    //     name: 'Opera',
    //     y: 1.40
    // }, {
    //     name: 'Sogou Explorer',
    //     y: 0.84
    // }, {
    //     name: 'QQ',
    //     y: 0.51
    // }, {
    //     name: 'Other',
    //     y: 2.6
    // }];
    Highcharts.chart(id, {
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
        },
        title: {
            text: 'Browser market shares in May, 2020',
            align: 'left'
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
                }
            }
        },
        series: [{
            name: 'Brands',
            colorByPoint: true,
            data: data
        }]
    });
}

window.getLocalStorageName = function () {
    return window.localStorage ? window.localStorage.name : null;
};

window.basicColumnChart = function (series) {
    Highcharts.chart('BasicColumnChart', {
        chart: {
            type: 'column'
        },
        title: {
            text: 'Top 5 selling products of current Year by each Month'
        },
        subtitle: {
            text: ''
        },
        xAxis: {
            categories: [
                'Jan',
                'Feb',
                'Mar',
                'Apr',
                'May',
                'Jun',
                'Jul',
                'Aug',
                'Sep',
                'Oct',
                'Nov',
                'Dec'
            ],
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
                '<td style="padding:0"><b>{point.y} qty</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0
            }
        },
        series: series
    });
}

window.barBasicChart = function (name, series) {
    Highcharts.chart('BarBasicChart', {
        chart: {
            type: 'bar'
        },
        title: {
            text: 'Historic World Population by Region',
            align: 'left'
        },
        subtitle: {
            text: 'Source: <a ' +
                'href="https://en.wikipedia.org/wiki/List_of_continents_and_continental_subregions_by_population"' +
                'target="_blank">Wikipedia.org</a>',
            align: 'left'
        },
        xAxis: {
            categories: name,
            title: {
                text: null
            },
            gridLineWidth: 1,
            lineWidth: 0
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Product Quantity',
                align: 'high'
            },
            labels: {
                overflow: 'justify'
            },
            gridLineWidth: 0
        },
        tooltip: {
            valueSuffix: ' millions'
        },
        plotOptions: {
            bar: {
                borderRadius: '50%',
                dataLabels: {
                    enabled: true
                },
                groupPadding: 0.1
            }
        },
        legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'top',
            x: -40,
            y: 80,
            floating: true,
            borderWidth: 1,
            backgroundColor:
                Highcharts.defaultOptions.legend.backgroundColor || '#FFFFFF',
            shadow: true
        },
        credits: {
            enabled: false
        },
        series: series
    });

}

window.columnRangeChart = function () {
    Highcharts.chart('ColumnRangeChart', {

        chart: {
            type: 'column',
            inverted: true
        },

        accessibility: {
            description: 'Image description: A column range chart compares the monthly temperature variations throughout 2017 in Vik I Sogn, Norway. The chart is interactive and displays the temperature range for each month when hovering over the data. The temperature is measured in degrees Celsius on the X-axis and the months are plotted on the Y-axis. The lowest temperature is recorded in March at minus 10.2 Celsius. The lowest range of temperatures is found in December ranging from a low of minus 9 to a high of 8.6 Celsius. The highest temperature is found in July at 26.2 Celsius. July also has the highest range of temperatures from 6 to 26.2 Celsius. The broadest range of temperatures is found in May ranging from a low of minus 0.6 to a high of 23.1 Celsius.'
        },

        title: {
            text: 'Temperature variation by month'
        },

        subtitle: {
            text: 'Observed in Vik i Sogn, Norway, 2021 | ' +
                'Source: <a href="https://www.vikjavev.no/ver/" target="_blank">Vikjavev</a>'
        },

        xAxis: {
            categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
                'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
        },

        yAxis: {
            title: {
                text: 'Temperature ( °C )'
            }
        },

        tooltip: {
            valueSuffix: '°C'
        },

        plotOptions: {
            columnrange: {
                borderRadius: '50%',
                dataLabels: {
                    enabled: true,
                    format: '{y}°C'
                }
            }
        },

        legend: {
            enabled: false
        },

        series: [{
            name: 'Temperatures',
            data: [
                [-13.9, 5.2],
                [-16.7, 10.6],
                [-4.7, 11.6],
                [-4.4, 16.8],
                [-2.1, 27.2],
                [5.9, 29.4],
                [6.5, 29.1],
                [4.7, 25.4],
                [4.3, 21.6],
                [-3.5, 15.1],
                [-9.8, 12.5],
                [-11.5, 8.4]
            ]
        }]

    });

}

window.pastFiveYear = function (data) {
    console.log(data);

    data = data.data;
    /*data = data.arrayObject;*/

    let lst = [];
    $.each(data, function (index, element) {
        lst.push([element.name, element.value])
    });
    console.log(lst);

    // Data retrieved from https://olympics.com/en/olympic-games/beijing-2022/medals
    Highcharts.chart('PastFiveYear', {
        chart: {
            type: 'pie',
            options3d: {
                enabled: true,
                alpha: 45
            }
        },
        title: {
            text: 'Past Five Years Sale Record',
            align: 'left'
        },
        subtitle: {
            text: '3D donut in Highcharts',
            align: 'left'
        },
        plotOptions: {
            pie: {
                innerSize: 100,
                depth: 45
            }
        },
        series: [{
            name: 'Year',
            data: lst
        }]
    });
}

window.pastSevenDays = function (days, series) {
    Highcharts.chart('PastSevenDays', {

        chart: {
            polar: true,
            type: 'line'
        },

        accessibility: {
            description: 'A spiderweb chart compares the allocated budget against actual spending within an organization. The spider chart has six spokes. Each spoke represents one of the 6 departments within the organization: sales, marketing, development, customer support, information technology and administration. The chart is interactive, and each data point is displayed upon hovering. The chart clearly shows that 4 of the 6 departments have overspent their budget with Marketing responsible for the greatest overspend of $20,000. The allocated budget and actual spending data points for each department are as follows: Sales. Budget equals $43,000; spending equals $50,000. Marketing. Budget equals $19,000; spending equals $39,000. Development. Budget equals $60,000; spending equals $42,000. Customer support. Budget equals $35,000; spending equals $31,000. Information technology. Budget equals $17,000; spending equals $26,000. Administration. Budget equals $10,000; spending equals $14,000.'
        },

        title: {
            text: 'Past seven days',
            x: -80
        },

        pane: {
            size: '80%'
        },

        xAxis: {
            categories: days,
            tickmarkPlacement: 'on',
            lineWidth: 0
        },

        yAxis: {
            gridLineInterpolation: 'polygon',
            lineWidth: 0,
            min: 0
        },

        tooltip: {
            shared: true,
            pointFormat: '<span style="color:{series.color}">{series.name}: <b>${point.y:,.0f}</b><br/>'
        },

        legend: {
            align: 'right',
            verticalAlign: 'middle',
            layout: 'vertical'
        },

        series: series,

        responsive: {
            rules: [{
                condition: {
                    maxWidth: 500
                },
                chartOptions: {
                    legend: {
                        align: 'center',
                        verticalAlign: 'bottom',
                        layout: 'horizontal'
                    },
                    pane: {
                        size: '70%'
                    }
                }
            }]
        }
    });
}
window.pastFiveYearFunnelChart = function (data) {
    // Set up the chart
    data = data.arrayObject
    /*console.log(data)*/
    Highcharts.chart('PastFiveYearFunnelChart', {
        chart: {
            type: 'funnel3d',
            options3d: {
                enabled: true,
                alpha: 10,
                depth: 50,
                viewDistance: 50
            }
        },
        title: {
            text: 'Highcharts Funnel3D Chart'
        },
        accessibility: {
            screenReaderSection: {
                beforeChartFormat: '<{headingTagName}>{chartTitle}</{headingTagName}><div>{typeDescription}</div><div>{chartSubtitle}</div><div>{chartLongdesc}</div>'
            }
        },
        plotOptions: {
            series: {
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b> ({point.y:,.0f})$',
                    allowOverlap: true,
                    y: 10
                },
                neckWidth: '30%',
                neckHeight: '25%',
                width: '80%',
                height: '80%'
            }
        },
        series: [{
            name: 'Year',
            data: data
        }]
    });
}

window.compareTwoYear = function (result, category) {
    //console.log(result)
    //console.log(category)
    Highcharts.chart('CompareTwoYear', {
        chart: {
            type: 'column'
        },
        title: {
            text: 'Efficiency Optimization by Branch'
        },
        xAxis: {
            categories: category
        },
        yAxis: [{
            min: 0,
            title: {
                text: 'Employees'
            }
        }, {
            title: {
                text: 'Profit (millions)'
            },
            opposite: true
        }],
        legend: {
            shadow: false
        },
        tooltip: {
            shared: true
        },
        plotOptions: {
            column: {
                grouping: false,
                shadow: false,
                borderWidth: 0
            }
        },
        series: result
    });
}

window.yearlySaleAmount = function (category, data) {
    // Set up the chart
    const chart = new Highcharts.Chart({
        chart: {
            renderTo: 'YearlySaleAmount',
            type: 'column',
            options3d: {
                enabled: true,
                alpha: 15,
                beta: 15,
                depth: 50,
                viewDistance: 25
            }
        },
        xAxis: {
            categories: category
        },
        yAxis: {
            title: {
                enabled: false
            }
        },
        tooltip: {
            headerFormat: '<b>{point.key}</b><br>',
            pointFormat: 'Amount: {point.y} MMK'
        },
        title: {
            text: 'Yearly Sale Amount From Current Year',
            align: 'left'
        },
        subtitle: {
            text: 'Source: ' +
                '<a href="https://ofv.no/registreringsstatistikk"' +
                'target="_blank">OFV</a>',
            align: 'left'
        },
        legend: {
            enabled: false
        },
        plotOptions: {
            column: {
                depth: 25
            }
        },
        series: [{
            data: data,
            colorByPoint: true
        }]
    });

    function showValues() {
        document.getElementById('alpha-value').innerHTML = chart.options.chart.options3d.alpha;
        document.getElementById('beta-value').innerHTML = chart.options.chart.options3d.beta;
        document.getElementById('depth-value').innerHTML = chart.options.chart.options3d.depth;
    }

    // Activate the sliders
    document.querySelectorAll('#sliders input').forEach(input => input.addEventListener('input', e => {
        chart.options.chart.options3d[e.target.id] = parseFloat(e.target.value);
        showValues();
        chart.redraw(false);
    }));

    showValues();

}
