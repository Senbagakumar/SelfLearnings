function BarChart(sector, subsector, level, assementId) {

    var bC1OrgScore = [];
    var bc4groups = [];
    $.ajax({
        type: "GET",
        url: API.SectorAloneOrganizationReport() + "/?sectorId=" + sector + "&subsectorId=" + subsector + "&level=" + level + "&assessementId=" + assementId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: OnSuccess_,
        error: OnErrorCall_
    });

    function OnSuccess_(response) {
        var oo = response.OtherOrg;
        var groups = response.Groups;

        for (var i = 0; i < oo.length; i++) {
            bC1OrgScore.push(oo[i]);
        }

        for (var j = 0; j < groups.length; j++) {
            bc4groups.push(groups[j]);
        }

        //bar chart
        var ctx1 = document.getElementById("barChart1");
        // ctx1.height = 100;
        var myChart1 = new Chart(ctx1, {
            type: 'bar',

            data: {
                labels: bc4groups,
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

        $('#1').show();
    }

    function OnErrorCall_(response) {
        alert("Whoops something went wrong!");
    }

    var bC3OrgScore = [];

    $.ajax({
        type: "GET",
        url: API.SectorAloneFinalReport() + "/?sectorId=" + sector + "&subsectorId=" + subsector + "&level=" + level + "&assessementId=" + assementId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: OnSuccess3_,
        error: OnErrorCall3_
    });

    function OnSuccess3_(response) {
        var oo = response.OtherOrg;

        for (var i = 0; i < oo.length; i++) {
            bC3OrgScore.push(oo[i]);
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

        $('#2').show();
        $('#load').hide();
    }

    function OnErrorCall3_(response) {
        alert("Whoops something went wrong!");
    }
}