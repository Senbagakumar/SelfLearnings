function GetUserReport(uid, level)
{
    "use strict";

    var bC1OrgScore = [];
    var bC1OtherOrgScore = [];

    $.ajax({
        type: "GET",
        url: API.OrganizationLevel1Report() + "/?id=" + uid + "&level=" + level,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: OnSuccess_,
        error: OnErrorCall_
    });

    function OnSuccess_(response) {
        var oo = response.OtherOrg;
        var og = response.Org;

        for (var i = 0; i < oo.length; i++) {
            bC1OtherOrgScore.push(oo[i]);
        }

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
                },
                {
                    label: "other organization Score",
                    data: bC1OtherOrgScore,
                    borderColor: "rgba(0,0,0,0.09)",
                    borderWidth: "0",
                    backgroundColor: "green"
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
    var bC3OtherOrgScore = [];

    $.ajax({
        type: "GET",
        url: API.OrganizationLevel1FinalReport() + "/?id=" + uid + "&level=" + level,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: OnSuccess3_,
        error: OnErrorCall3_
    });

    function OnSuccess3_(response) {
        var oo = response.OtherOrg;
        var og = response.Org;

        for (var i = 0; i < oo.length; i++) {
            bC3OtherOrgScore.push(oo[i]);
        }

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
                },
                {
                    label: "other organization Score",
                    data: bC3OtherOrgScore,
                    borderColor: "rgba(0,0,0,0.09)",
                    borderWidth: "0",
                    backgroundColor: "green"
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

    var bC4OrgScore = [];
    var bC4OtherOrgScore = [];

    $.ajax({
        type: "GET",
        url: API.SectorLevel1Report() + "/?id=" + uid + "&level=" + level,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: OnSuccess4_,
        error: OnErrorCall4_
    });

    function OnSuccess4_(response) {
        var oo = response.OtherOrg;
        var og = response.Org;

        for (var i = 0; i < oo.length; i++) {
            bC4OtherOrgScore.push(oo[i]);
        }

        for (var i = 0; i < og.length; i++) {
            bC4OrgScore.push(og[i]);
        }
    }

    function OnErrorCall4_(response) {
        alert("Whoops something went wrong!");
    }

    //bar chart
    var ctx4 = document.getElementById("barChart4");
    // ctx4.height = 100;
    var myChart4 = new Chart(ctx4, {
        type: 'bar',
        data: {
            labels: ["group1", "group2", "group3", "group4", "group5", "group6", "group7", "group8", "group9"],
            datasets: [
                {
                    label: "organization individual Score",
                    data: bC4OrgScore,
                    borderColor: "rgba(0, 123, 255, 0.9)",
                    borderWidth: "0",
                    backgroundColor: "rgba(0, 123, 255, 0.5)"
                },
                {
                    label: "manufacure individual Score",
                    data: bC4OtherOrgScore,
                    borderColor: "rgba(0,0,0,0.09)",
                    borderWidth: "0",
                    backgroundColor: "green"
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
                    }
                }]
            }
        }
    });

    var bC5OrgScore = [];
    var bC5OtherOrgScore = [];

    $.ajax({
        type: "GET",
        url: API.SectorLevel1FinalReport() + "/?id=" + uid + "&level=" + level,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: OnSuccess5_,
        error: OnErrorCall5_
    });

    function OnSuccess5_(response) {
        var oo = response.OtherOrg;
        var og = response.Org;

        for (var i = 0; i < oo.length; i++) {
            bC5OtherOrgScore.push(oo[i]);
        }

        for (var i = 0; i < og.length; i++) {
            bC5OrgScore.push(og[i]);
        }
    }

    function OnErrorCall5_(response) {
        alert("Whoops something went wrong!");
    }

    //bar chart
    var ctx5 = document.getElementById("barChart5");
    //    ctx.height = 200;
    var myChart5 = new Chart(ctx5, {
        type: 'bar',
        data: {
            labels: ["Final Score"],
            datasets: [
                {

                    label: "organization individual Score",
                    data: bC5OrgScore,
                    borderColor: "rgba(0, 123, 255, 0.9)",
                    borderWidth: "0",
                    backgroundColor: "rgba(0, 123, 255, 0.5)"
                },
                {
                    label: "manufacture individual Score",
                    data: bC5OtherOrgScore,
                    borderColor: "rgba(0,0,0,0.09)",
                    borderWidth: "0",
                    backgroundColor: "green"
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

};