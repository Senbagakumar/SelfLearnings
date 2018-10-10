(function ($) {
    "use strict";

    var bC21OrgScore = [];
    var bC21OtherOrgScore = [];

    $.ajax({
        type: "GET",
        url: API.OrganizationLevel2Report,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: OnSuccess21_,
        error: OnErrorCall21_
    });

    function OnSuccess21_(response) {
        var oo = response.OtherOrg;
        var og = response.Org;

        for (var i = 0; i < oo.length; i++) {
            bC21OtherOrgScore.push(oo[i]);
        }

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
                },
                {
                    label: "other organization Score",
                    data: bC21OtherOrgScore,
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


    var bC23OrgScore = [];
    var bC23OtherOrgScore = [];

    $.ajax({
        type: "GET",
        url: API.OrganizationLevel2FinalReport,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: OnSuccess23_,
        error: OnErrorCall23_
    });

    function OnSuccess23_(response) {
        var oo = response.OtherOrg;
        var og = response.Org;

        for (var i = 0; i < oo.length; i++) {
            bC23OtherOrgScore.push(oo[i]);
        }

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
                },
                {
                    label: "other organization Score",
                    data: bC23OtherOrgScore,
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

    var bC24OrgScore = [];
    var bC24OtherOrgScore = [];

    $.ajax({
        type: "GET",
        url: API.SectorLevel2Report,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: OnSuccess24_,
        error: OnErrorCall24_
    });

    function OnSuccess24_(response) {
        var oo = response.OtherOrg;
        var og = response.Org;

        for (var i = 0; i < oo.length; i++) {
            bC24OtherOrgScore.push(oo[i]);
        }

        for (var i = 0; i < og.length; i++) {
            bC24OrgScore.push(og[i]);
        }
    }

    function OnErrorCall24_(response) {
        alert("Whoops something went wrong!");
    }

    //bar chart
    var ctx4 = document.getElementById("barChart24");
    // ctx4.height = 100;
    var myChart4 = new Chart(ctx4, {
        type: 'bar',
        data: {
            labels: ["group1", "group2", "group3", "group4", "group5", "group6", "group7", "group8", "group9"],
            datasets: [
                {
                    label: "organization individual Score",
                    data: bC24OrgScore,
                    borderColor: "rgba(0, 123, 255, 0.9)",
                    borderWidth: "0",
                    backgroundColor: "rgba(0, 123, 255, 0.5)"
                },
                {
                    label: "manufacure individual Score",
                    data: bC24OtherOrgScore,
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

    var bC25OrgScore = [];
    var bC25OtherOrgScore = [];

    $.ajax({
        type: "GET",
        url: API.SectorLevel2FinalReport,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: OnSuccess25_,
        error: OnErrorCall25_
    });

    function OnSuccess25_(response) {
        var oo = response.OtherOrg;
        var og = response.Org;

        for (var i = 0; i < oo.length; i++) {
            bC25OtherOrgScore.push(oo[i]);
        }

        for (var i = 0; i < og.length; i++) {
            bC25OrgScore.push(og[i]);
        }
    }

    function OnErrorCall25_(response) {
        alert("Whoops something went wrong!");
    }

    //bar chart
    var ctx5 = document.getElementById("barChart25");
    //    ctx.height = 200;
    var myChart5 = new Chart(ctx5, {
        type: 'bar',
        data: {
            labels: ["Final Score"],
            datasets: [
                {

                    label: "organization individual Score",
                    data: bC25OrgScore,
                    borderColor: "rgba(0, 123, 255, 0.9)",
                    borderWidth: "0",
                    backgroundColor: "rgba(0, 123, 255, 0.5)"
                },
                {
                    label: "manufacture individual Score",
                    data: bC25OtherOrgScore,
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

})(jQuery);