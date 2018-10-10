(function ($) {
    "use strict";

    var bC1OrgScore = [];
    $.ajax({
        type: "GET",
        url: API.SectorLevel1Report,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: OnSuccess_,
        error: OnErrorCall_
    });

    function OnSuccess_(response) {
        var oo = response.OtherOrg;
        var og = response.Org;

        for (var i = 0; i < og.length; i++) {
            bC1OrgScore.push(og[i]);
        }
    }

    function OnErrorCall_(response) {
        alert("Whoops something went wrong!");
    }

    //bar chart
    var ctx1 = document.getElementById("barChart1");
    // ctx1.height = 100;
    var myChart1 = new Chart(ctx1, {
        type: 'bar',

        data: {
            labels: ["group1", "group2", "group3", "group4", "group5", "group6", "group7", "group8", "group9"],
            datasets: [
                {

                    label: "organization Score",
                    data: bC1OrgScore,
                    borderColor: "rgba(0, 123, 255, 0.9)",
                    borderWidth: "0",
                    backgroundColor: "rgba(0, 123, 255, 0.5)"
                }

            ]
        },

        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        //beginAtZero: true
                        min: 0,
                        max: 100,
                        stepSize: 20

                        //steps: 100,
                        //stepValue: 100,
                        //max: 40 
                    }
                }]
            }
        }
    });

    var bC3OrgScore = [];

    $.ajax({
        type: "GET",
        url: API.SectorLevel1FinalReport,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: OnSuccess3_,
        error: OnErrorCall3_
    });

    function OnSuccess3_(response) {
        var oo = response.OtherOrg;
        var og = response.Org;

        for (var i = 0; i < og.length; i++) {
            bC3OrgScore.push(og[i]);
        }
    }

    function OnErrorCall3_(response) {
        alert("Whoops something went wrong!");
    }

    //bar chart
    var ctx3 = document.getElementById("barChart3");
    //    ctx.height = 200;
    var myChart3 = new Chart(ctx3, {
        type: 'bar',
        data: {
            labels: ["Final Score"],
            datasets: [
                {

                    label: "organization Score",
                    data: bC3OrgScore,
                    borderColor: "rgba(0, 123, 255, 0.9)",
                    borderWidth: "0",
                    backgroundColor: "rgba(0, 123, 255, 0.5)"
                }
            ]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        min: 0,
                        max: 1000,
                        stepSize: 100
                    }
                }]
            }
        }
    });


    var bC21OrgScore = [];
    $.ajax({
        type: "GET",
        url: API.SectorLevel2Report,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: OnSuccess21_,
        error: OnErrorCall21_
    });

    function OnSuccess21_(response) {
        var oo = response.OtherOrg;
        var og = response.Org;

        for (var i = 0; i < og.length; i++) {
            bC21OrgScore.push(og[i]);
        }
    }

    function OnErrorCall21_(response) {
        alert("Whoops something went wrong!");
    }

    //bar chart
    var ctx1 = document.getElementById("barChart21");
    // ctx1.height = 100;
    var myChart1 = new Chart(ctx1, {
        type: 'bar',

        data: {
            labels: ["group1", "group2", "group3", "group4", "group5", "group6", "group7", "group8", "group9"],
            datasets: [
                {

                    label: "organization Score",
                    data: bC21OrgScore,
                    borderColor: "rgba(0, 123, 255, 0.9)",
                    borderWidth: "0",
                    backgroundColor: "rgba(0, 123, 255, 0.5)"
                }

            ]
        },

        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        //beginAtZero: true
                        min: 0,
                        max: 100,
                        stepSize: 20

                        //steps: 100,
                        //stepValue: 100,
                        //max: 40 
                    }
                }]
            }
        }
    });

    var bC23OrgScore = [];

    $.ajax({
        type: "GET",
        url: API.SectorLevel2FinalReport,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: OnSuccess23_,
        error: OnErrorCall23_
    });

    function OnSuccess23_(response) {
        var oo = response.OtherOrg;
        var og = response.Org;

        for (var i = 0; i < og.length; i++) {
            bC23OrgScore.push(og[i]);
        }
    }

    function OnErrorCall23_(response) {
        alert("Whoops something went wrong!");
    }

    //bar chart
    var ctx3 = document.getElementById("barChart23");
    //    ctx.height = 200;
    var myChart3 = new Chart(ctx3, {
        type: 'bar',
        data: {
            labels: ["Final Score"],
            datasets: [
                {

                    label: "organization Score",
                    data: bC23OrgScore,
                    borderColor: "rgba(0, 123, 255, 0.9)",
                    borderWidth: "0",
                    backgroundColor: "rgba(0, 123, 255, 0.5)"
                }
            ]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        min: 0,
                        max: 1000,
                        stepSize: 100
                    }
                }]
            }
        }
    });



    var bC31OrgScore = [];
    $.ajax({
        type: "GET",
        url: API.SectorLevel3Report,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: OnSuccess31_,
        error: OnErrorCall31_
    });

    function OnSuccess31_(response) {
        var oo = response.OtherOrg;
        var og = response.Org;

        for (var i = 0; i < og.length; i++) {
            bC31OrgScore.push(og[i]);
        }
    }

    function OnErrorCall31_(response) {
        alert("Whoops something went wrong!");
    }

    //bar chart
    var ctx1 = document.getElementById("barChart31");
    // ctx1.height = 100;
    var myChart1 = new Chart(ctx1, {
        type: 'bar',

        data: {
            labels: ["group1", "group2", "group3", "group4", "group5", "group6", "group7", "group8", "group9"],
            datasets: [
                {

                    label: "organization Score",
                    data: bC31OrgScore,
                    borderColor: "rgba(0, 123, 255, 0.9)",
                    borderWidth: "0",
                    backgroundColor: "rgba(0, 123, 255, 0.5)"
                }

            ]
        },

        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        //beginAtZero: true
                        min: 0,
                        max: 100,
                        stepSize: 20

                        //steps: 100,
                        //stepValue: 100,
                        //max: 40 
                    }
                }]
            }
        }
    });

    var bC33OrgScore = [];

    $.ajax({
        type: "GET",
        url: API.SectorLevel3FinalReport,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: OnSuccess33_,
        error: OnErrorCall33_
    });

    function OnSuccess33_(response) {
        var oo = response.OtherOrg;
        var og = response.Org;

        for (var i = 0; i < og.length; i++) {
            bC33OrgScore.push(og[i]);
        }
    }

    function OnErrorCall33_(response) {
        alert("Whoops something went wrong!");
    }

    //bar chart
    var ctx3 = document.getElementById("barChart33");
    //    ctx.height = 200;
    var myChart3 = new Chart(ctx3, {
        type: 'bar',
        data: {
            labels: ["Final Score"],
            datasets: [
                {

                    label: "organization Score",
                    data: bC33OrgScore,
                    borderColor: "rgba(0, 123, 255, 0.9)",
                    borderWidth: "0",
                    backgroundColor: "rgba(0, 123, 255, 0.5)"
                }
            ]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        min: 0,
                        max: 1000,
                        stepSize: 100
                    }
                }]
            }
        }
    });



})(jQuery);