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
    console.log({id, data});
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



window.sixMostSoldProducts = function (sixSeries) {
    Highcharts.chart('SixMostSoldProducts', {
        chart: {
            type: 'pictorial'
        },

        colors: ['#B0FDFE', '#E3FED4', '#F9F492', '#FAF269', '#FAE146', '#FDA003'],

        title: {
            text: 'Six Most Sold Products'
        },

        subtitle: {
            text: 'Source: ' +
                '<a href="https://en.wikipedia.org/wiki/Color_temperature"' +
                'target="_blank">Wikipedia.org</a> '
        },

        accessibility: {
            screenReaderSection: {
                beforeChartFormat: '<{headingTagName}>{chartTitle}</{headingTagName}><p>{typeDescription}</p><p>{chartSubtitle}</p><p>{chartLongdesc}</p>'
            },
            point: {
                valueDescriptionFormat: '{value}.'
            },
            series: {
                descriptionFormat: ''
            },
            landmarkVerbosity: 'one'
        },

        xAxis: {
            visible: false,
            min: 0.2
        },

        yAxis: {
            visible: false
        },

        legend: {
            align: 'right',
            floating: true,
            itemMarginTop: 5,
            itemMarginBottom: 5,
            layout: 'vertical',
            margin: 0,
            padding: 0,
            verticalAlign: 'middle'
        },

        tooltip: {
            headerFormat: '',
            valueSuffix: ' K'
        },

        plotOptions: {
            series: {
                pointPadding: 0,
                groupPadding: 0,
                borderWidth: 0,
                dataLabels: {
                    enabled: true,
                    align: 'center',
                    format: '{y} qty'
                },
                stacking: 'percent',
                paths: [{
                    definition: 'M480.15 0.510986V0.531986C316.002 0.531986 197.223 56.655 119.105 139.78C40.987 222.905 3.50699 332.801 0.884992 440.062C-1.74001 547.459 36.194 644.769 79.287 725.354C122.38 805.938 170.742 870.203 188.861 909.922C205.994 947.479 203.626 990.232 206.788 1033.17C209.95 1076.11 219.126 1119.48 260.261 1156.26C260.888 1156.83 261.679 1157.18 262.52 1157.27C262.639 1157.28 262.75 1157.28 262.87 1157.29L262.747 1173.69L274.021 1200.24C275.812 1214.45 275.053 1222.2 273.364 1229.45C261.44 1238.59 250.866 1253.57 283.323 1261.97V1283.88C249.425 1299.28 261.103 1315.14 283.323 1327.03L281.331 1342.96C249.673 1354.72 261.6 1377.5 282.645 1388.76V1403.36C256.094 1414.86 256.771 1436.12 283.323 1451.16V1473.73L349.035 1535.46L396.163 1582.58L397.498 1600.51H565.433V1585.91L619.193 1535.46C631.786 1531.75 660.881 1505.66 698.191 1468.41L702.729 1451.49L686.753 1440.38L687.226 1426.38C714.969 1420.61 718.256 1388.06 687.226 1382.78V1366.87C725.039 1359.03 715.965 1331.13 690.532 1325.04V1311.77C735.92 1292.94 715.774 1272.19 695.193 1267.29V1245.38C721.584 1240.94 721.209 1210.5 702.688 1201.19L711.107 1183.45L711.682 1162.54C713.198 1162.5 714.725 1162.46 716.241 1162.38C717.056 1162.36 717.845 1162.09 718.5 1161.6C754.295 1134.83 762.81 1094.37 765.299 1051.47C767.789 1008.58 764.577 962.629 775.69 923.173C788.878 876.344 833.216 822.264 875.654 750.885C918.093 679.505 958.46 590.459 963.133 472.719C967.812 354.836 929.374 236.776 848.507 148.143C767.638 59.511 644.344 0.516987 480.15 0.516987V0.510986Z'
                }]
            }
        },
        series: sixSeries
    });
}

window.maxMinQtyOfProducts = function (data) {
    Highcharts.chart('MaxMinQtyOfProducts', {

        chart: {
            type: 'dumbbell',
            inverted: true
        },

        legend: {
            enabled: false
        },

        subtitle: {
            text: '1970 vs 2021 Source: ' +
                '<a href="https://ec.europa.eu/eurostat/en/web/main/data/database"' +
                'target="_blank">Eurostat</a>'
        },

        title: {
            text: 'Maximum - Minimum Quantity of Products'
        },

        tooltip: {
            shared: true
        },

        xAxis: {
            type: 'category'
        },

        yAxis: {
            title: {
                text: 'Quantity'
            }
        },

        series: [{
            name: 'Quantity',
            data: data
        }]

    });
}

/*window.productCategoryAndProduct = function () {
    Highcharts.chart('ProductCategoryAndProduct', {
        chart: {
            type: 'packedbubble',
            height: '100%'
        },
        title: {
            text: 'Carbon emissions around the world (2014)',
            align: 'left'
        },
        tooltip: {
            useHTML: true,
            pointFormat: '<b>{point.name}:</b> {point.value}m CO<sub>2</sub>'
        },
        plotOptions: {
            packedbubble: {
                minSize: '20%',
                maxSize: '100%',
                zMin: 0,
                zMax: 1000,
                layoutAlgorithm: {
                    gravitationalConstant: 0.05,
                    splitSeries: true,
                    seriesInteraction: false,
                    dragBetweenSeries: true,
                    parentNodeLimit: true
                },
                dataLabels: {
                    enabled: true,
                    format: '{point.name}',
                    filter: {
                        property: 'y',
                        operator: '>',
                        value: 250
                    },
                    style: {
                        color: 'black',
                        textOutline: 'none',
                        fontWeight: 'normal'
                    }
                }
            }
        },
        series: [{
            name: 'Europe',
            data: [{
                name: 'Germany',
                value: 767.1
            }, {
                name: 'Croatia',
                value: 20.7
            },
                {
                    name: 'Belgium',
                    value: 97.2
                },
                {
                    name: 'Czech Republic',
                    value: 111.7
                },
                {
                    name: 'Netherlands',
                    value: 158.1
                },
                {
                    name: 'Spain',
                    value: 241.6
                },
                {
                    name: 'Ukraine',
                    value: 249.1
                },
                {
                    name: 'Poland',
                    value: 298.1
                },
                {
                    name: 'France',
                    value: 323.7
                },
                {
                    name: 'Romania',
                    value: 78.3
                },
                {
                    name: 'United Kingdom',
                    value: 415.4
                }, {
                    name: 'Turkey',
                    value: 353.2
                }, {
                    name: 'Italy',
                    value: 337.6
                },
                {
                    name: 'Greece',
                    value: 71.1
                },
                {
                    name: 'Austria',
                    value: 69.8
                },
                {
                    name: 'Belarus',
                    value: 67.7
                },
                {
                    name: 'Serbia',
                    value: 59.3
                },
                {
                    name: 'Finland',
                    value: 54.8
                },
                {
                    name: 'Bulgaria',
                    value: 51.2
                },
                {
                    name: 'Portugal',
                    value: 48.3
                },
                {
                    name: 'Norway',
                    value: 44.4
                },
                {
                    name: 'Sweden',
                    value: 44.3
                },
                {
                    name: 'Hungary',
                    value: 43.7
                },
                {
                    name: 'Switzerland',
                    value: 40.2
                },
                {
                    name: 'Denmark',
                    value: 40
                },
                {
                    name: 'Slovakia',
                    value: 34.7
                },
                {
                    name: 'Ireland',
                    value: 34.6
                },
                {
                    name: 'Croatia',
                    value: 20.7
                },
                {
                    name: 'Estonia',
                    value: 19.4
                },
                {
                    name: 'Slovenia',
                    value: 16.7
                },
                {
                    name: 'Lithuania',
                    value: 12.3
                },
                {
                    name: 'Luxembourg',
                    value: 10.4
                },
                {
                    name: 'Macedonia',
                    value: 9.5
                },
                {
                    name: 'Moldova',
                    value: 7.8
                },
                {
                    name: 'Latvia',
                    value: 7.5
                },
                {
                    name: 'Cyprus',
                    value: 7.2
                }]
        }, {
            name: 'Africa',
            data: [{
                name: 'Senegal',
                value: 8.2
            },
                {
                    name: 'Cameroon',
                    value: 9.2
                },
                {
                    name: 'Zimbabwe',
                    value: 13.1
                },
                {
                    name: 'Ghana',
                    value: 14.1
                },
                {
                    name: 'Kenya',
                    value: 14.1
                },
                {
                    name: 'Sudan',
                    value: 17.3
                },
                {
                    name: 'Tunisia',
                    value: 24.3
                },
                {
                    name: 'Angola',
                    value: 25
                },
                {
                    name: 'Libya',
                    value: 50.6
                },
                {
                    name: 'Ivory Coast',
                    value: 7.3
                },
                {
                    name: 'Morocco',
                    value: 60.7
                },
                {
                    name: 'Ethiopia',
                    value: 8.9
                },
                {
                    name: 'United Republic of Tanzania',
                    value: 9.1
                },
                {
                    name: 'Nigeria',
                    value: 93.9
                },
                {
                    name: 'South Africa',
                    value: 392.7
                }, {
                    name: 'Egypt',
                    value: 225.1
                }, {
                    name: 'Algeria',
                    value: 141.5
                }]
        }, {
            name: 'Oceania',
            data: [{
                name: 'Australia',
                value: 409.4
            },
                {
                    name: 'New Zealand',
                    value: 34.1
                },
                {
                    name: 'Papua New Guinea',
                    value: 7.1
                }]
        }, {
            name: 'North America',
            data: [{
                name: 'Costa Rica',
                value: 7.6
            },
                {
                    name: 'Honduras',
                    value: 8.4
                },
                {
                    name: 'Jamaica',
                    value: 8.3
                },
                {
                    name: 'Panama',
                    value: 10.2
                },
                {
                    name: 'Guatemala',
                    value: 12
                },
                {
                    name: 'Dominican Republic',
                    value: 23.4
                },
                {
                    name: 'Cuba',
                    value: 30.2
                },
                {
                    name: 'USA',
                    value: 5334.5
                }, {
                    name: 'Canada',
                    value: 566
                }, {
                    name: 'Mexico',
                    value: 456.3
                }]
        }, {
            name: 'South America',
            data: [{
                name: 'El Salvador',
                value: 7.2
            },
                {
                    name: 'Uruguay',
                    value: 8.1
                },
                {
                    name: 'Bolivia',
                    value: 17.8
                },
                {
                    name: 'Trinidad and Tobago',
                    value: 34
                },
                {
                    name: 'Ecuador',
                    value: 43
                },
                {
                    name: 'Chile',
                    value: 78.6
                },
                {
                    name: 'Peru',
                    value: 52
                },
                {
                    name: 'Colombia',
                    value: 74.1
                },
                {
                    name: 'Brazil',
                    value: 501.1
                }, {
                    name: 'Argentina',
                    value: 199
                },
                {
                    name: 'Venezuela',
                    value: 195.2
                }]
        }, {
            name: 'Asia',
            data: [{
                name: 'Nepal',
                value: 6.5
            },
                {
                    name: 'Georgia',
                    value: 6.5
                },
                {
                    name: 'Brunei Darussalam',
                    value: 7.4
                },
                {
                    name: 'Kyrgyzstan',
                    value: 7.4
                },
                {
                    name: 'Afghanistan',
                    value: 7.9
                },
                {
                    name: 'Myanmar',
                    value: 9.1
                },
                {
                    name: 'Mongolia',
                    value: 14.7
                },
                {
                    name: 'Sri Lanka',
                    value: 16.6
                },
                {
                    name: 'Bahrain',
                    value: 20.5
                },
                {
                    name: 'Yemen',
                    value: 22.6
                },
                {
                    name: 'Jordan',
                    value: 22.3
                },
                {
                    name: 'Lebanon',
                    value: 21.1
                },
                {
                    name: 'Azerbaijan',
                    value: 31.7
                },
                {
                    name: 'Singapore',
                    value: 47.8
                },
                {
                    name: 'Hong Kong',
                    value: 49.9
                },
                {
                    name: 'Syria',
                    value: 52.7
                },
                {
                    name: 'DPR Korea',
                    value: 59.9
                },
                {
                    name: 'Israel',
                    value: 64.8
                },
                {
                    name: 'Turkmenistan',
                    value: 70.6
                },
                {
                    name: 'Oman',
                    value: 74.3
                },
                {
                    name: 'Qatar',
                    value: 88.8
                },
                {
                    name: 'Philippines',
                    value: 96.9
                },
                {
                    name: 'Kuwait',
                    value: 98.6
                },
                {
                    name: 'Uzbekistan',
                    value: 122.6
                },
                {
                    name: 'Iraq',
                    value: 139.9
                },
                {
                    name: 'Pakistan',
                    value: 158.1
                },
                {
                    name: 'Vietnam',
                    value: 190.2
                },
                {
                    name: 'United Arab Emirates',
                    value: 201.1
                },
                {
                    name: 'Malaysia',
                    value: 227.5
                },
                {
                    name: 'Kazakhstan',
                    value: 236.2
                },
                {
                    name: 'Thailand',
                    value: 272
                },
                {
                    name: 'Taiwan',
                    value: 276.7
                },
                {
                    name: 'Indonesia',
                    value: 453
                },
                {
                    name: 'Saudi Arabia',
                    value: 494.8
                },
                {
                    name: 'Japan',
                    value: 1278.9
                },
                {
                    name: 'China',
                    value: 10540.8
                },
                {
                    name: 'India',
                    value: 2341.9
                },
                {
                    name: 'Russia',
                    value: 1766.4
                },
                {
                    name: 'Iran',
                    value: 618.2
                },
                {
                    name: 'Korea',
                    value: 610.1
                }]
        }]
    });
}*/

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
