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

window.columnChart = function () {
    Highcharts.chart('ColumnChart', {

        chart: {
            type: 'column'
        },

        title: {
            text: 'Olympic Games all-time medal table, grouped by continent',
            align: 'left'
        },

        xAxis: {
            categories: ['Gold', 'Silver', 'Bronze']
        },

        yAxis: {
            allowDecimals: false,
            min: 0,
            title: {
                text: 'Count medals'
            }
        },

        tooltip: {
            format: '<b>{key}</b><br/>{series.name}: {y}<br/>' +
                'Total: {point.stackTotal}'
        },

        plotOptions: {
            column: {
                stacking: 'normal'
            }
        },

        series: [{
            name: 'Norway',
            data: [148, 133, 124],
            stack: 'Europe'
        }, {
            name: 'Germany',
            data: [102, 98, 65],
            stack: 'Europe'
        }, {
            name: 'United States',
            data: [113, 122, 95],
            stack: 'North America'
        }, {
            name: 'Canada',
            data: [77, 72, 80],
            stack: 'North America'
        }]
    });
}

window.basicColumnChart = function () {
    Highcharts.chart('BasicColumnChart', {
        chart: {
            type: 'column'
        },
        title: {
            text: 'Monthly Average Rainfall'
        },
        subtitle: {
            text: 'Source: WorldClimate.com'
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
                text: 'Rainfall (mm)'
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.1f} mm</b></td></tr>',
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
        series: [{
            name: 'Tokyo',
            data: [49.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4,
                194.1, 95.6, 54.4]

        }, {
            name: 'New York',
            data: [83.6, 78.8, 98.5, 93.4, 106.0, 84.5, 105.0, 104.3, 91.2, 83.5,
                106.6, 92.3]

        }, {
            name: 'London',
            data: [48.9, 38.8, 39.3, 41.4, 47.0, 48.3, 59.0, 59.6, 52.4, 65.2, 59.3,
                51.2]

        }, {
            name: 'Berlin',
            data: [42.4, 33.2, 34.5, 39.7, 52.6, 75.5, 57.4, 60.4, 47.6, 39.1, 46.8,
                51.1]

        }]
    });
}

window.barBasicChart = function () {
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
            categories: ['Africa', 'America', 'Asia', 'Europe', 'Oceania'],
            title: {
                text: null
            },
            gridLineWidth: 1,
            lineWidth: 0
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Population (millions)',
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
        series: [{
            name: 'Year 1990',
            data: [631, 727, 3202, 721, 26]
        }, {
            name: 'Year 2000',
            data: [814, 841, 3714, 726, 31]
        }, {
            name: 'Year 2010',
            data: [1044, 944, 4170, 735, 40]
        }, {
            name: 'Year 2018',
            data: [1276, 1007, 4561, 746, 42]
        }]
    });

}

window.barRaceChart = function () {
    const startYear = 1960,
        endYear = 2018,
        btn = document.getElementById('play-pause-button'),
        input = document.getElementById('play-range'),
        nbr = 20;

    let dataset, chart;


    /*
     * Animate dataLabels functionality
     */
    (function (H) {
        const FLOAT = /^-?\d+\.?\d*$/;

        // Add animated textSetter, just like fill/strokeSetters
        H.Fx.prototype.textSetter = function () {
            let startValue = this.start.replace(/ /g, ''),
                endValue = this.end.replace(/ /g, ''),
                currentValue = this.end.replace(/ /g, '');

            if ((startValue || '').match(FLOAT)) {
                startValue = parseInt(startValue, 10);
                endValue = parseInt(endValue, 10);

                // No support for float
                currentValue = Highcharts.numberFormat(
                    Math.round(startValue + (endValue - startValue) * this.pos),
                    0
                );
            }

            this.elem.endText = this.end;

            this.elem.attr(this.prop, currentValue, null, true);
        };

        // Add textGetter, not supported at all at this moment:
        H.SVGElement.prototype.textGetter = function () {
            const ct = this.text.element.textContent || '';
            return this.endText ? this.endText : ct.substring(0, ct.length / 2);
        };

        // Temporary change label.attr() with label.animate():
        // In core it's simple change attr(...) => animate(...) for text prop
        H.wrap(H.Series.prototype, 'drawDataLabels', function (proceed) {
            const attr = H.SVGElement.prototype.attr,
                chart = this.chart;

            if (chart.sequenceTimer) {
                this.points.forEach(point =>
                    (point.dataLabels || []).forEach(
                        label =>
                        (label.attr = function (hash) {
                            if (
                                hash &&
                                hash.text !== undefined &&
                                chart.isResizing === 0
                            ) {
                                const text = hash.text;

                                delete hash.text;

                                return this
                                    .attr(hash)
                                    .animate({ text });
                            }
                            return attr.apply(this, arguments);

                        })
                    )
                );
            }

            const ret = proceed.apply(
                this,
                Array.prototype.slice.call(arguments, 1)
            );

            this.points.forEach(p =>
                (p.dataLabels || []).forEach(d => (d.attr = attr))
            );

            return ret;
        });
    }(Highcharts));


    function getData(year) {
        const output = Object.entries(dataset)
            .map(country => {
                const [countryName, countryData] = country;
                return [countryName, Number(countryData[year])];
            })
            .sort((a, b) => b[1] - a[1]);
        return [output[0], output.slice(1, nbr)];
    }

    function getSubtitle() {
        const population = (getData(input.value)[0][1] / 1000000000).toFixed(2);
        return `<span style="font-size: 80px">${input.value}</span>
        <br>
        <span style="font-size: 22px">
            Total: <b>: ${population}</b> billion
        </span>`;
    }

    (async () => {

        dataset = await fetch(
            'https://demo-live-data.highcharts.com/population.json'
        ).then(response => response.json());


        chart = Highcharts.chart('BarRaceChart', {
            chart: {
                animation: {
                    duration: 500
                },
                marginRight: 50
            },
            title: {
                text: 'World population by country',
                align: 'left'
            },
            subtitle: {
                useHTML: true,
                text: getSubtitle(),
                floating: true,
                align: 'right',
                verticalAlign: 'middle',
                y: -80,
                x: -100
            },

            legend: {
                enabled: false
            },
            xAxis: {
                type: 'category'
            },
            yAxis: {
                opposite: true,
                tickPixelInterval: 150,
                title: {
                    text: null
                }
            },
            plotOptions: {
                series: {
                    animation: false,
                    groupPadding: 0,
                    pointPadding: 0.1,
                    borderWidth: 0,
                    colorByPoint: true,
                    dataSorting: {
                        enabled: true,
                        matchByName: true
                    },
                    type: 'bar',
                    dataLabels: {
                        enabled: true
                    }
                }
            },
            series: [
                {
                    type: 'bar',
                    name: startYear,
                    data: getData(startYear)[1]
                }
            ],
            responsive: {
                rules: [{
                    condition: {
                        maxWidth: 550
                    },
                    chartOptions: {
                        xAxis: {
                            visible: false
                        },
                        subtitle: {
                            x: 0
                        },
                        plotOptions: {
                            series: {
                                dataLabels: [{
                                    enabled: true,
                                    y: 8
                                }, {
                                    enabled: true,
                                    format: '{point.name}',
                                    y: -8,
                                    style: {
                                        fontWeight: 'normal',
                                        opacity: 0.7
                                    }
                                }]
                            }
                        }
                    }
                }]
            }
        });
    })();

    /*
     * Pause the timeline, either when the range is ended, or when clicking the pause button.
     * Pausing stops the timer and resets the button to play mode.
     */
    function pause(button) {
        button.title = 'play';
        button.className = 'fa fa-play';
        clearTimeout(chart.sequenceTimer);
        chart.sequenceTimer = undefined;
    }

    /*
     * Update the chart. This happens either on updating (moving) the range input,
     * or from a timer when the timeline is playing.
     */
    function update(increment) {
        if (increment) {
            input.value = parseInt(input.value, 10) + increment;
        }
        if (input.value >= endYear) {
            // Auto-pause
            pause(btn);
        }

        chart.update(
            {
                subtitle: {
                    text: getSubtitle()
                }
            },
            false,
            false,
            false
        );

        chart.series[0].update({
            name: input.value,
            data: getData(input.value)[1]
        });
    }

    /*
     * Play the timeline.
     */
    function play(button) {
        button.title = 'pause';
        button.className = 'fa fa-pause';
        chart.sequenceTimer = setInterval(function () {
            update(1);
        }, 500);
    }

    btn.addEventListener('click', function () {
        if (chart.sequenceTimer) {
            pause(this);
        } else {
            play(this);
        }
    });
    /*
     * Trigger the update on the range bar click.
     */
    input.addEventListener('click', function () {
        update();
    });

}