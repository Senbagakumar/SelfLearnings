( function ( $ ) {
    "use strict";

	
	 //bar chart
    var ctx1 = document.getElementById( "barChart1" );
      // ctx1.height = 100;
    var myChart1 = new Chart( ctx1, {
        type: 'bar',
		
        data: {
            labels: [ "group1", "group2", "group3","group4","group5","group6","group7","group8","group9"],
            datasets: [
                {
					
                    label: "organization Score",
                    data: [ 25, 90, 50,56,22,36,85,20,10 ],
                    borderColor: "rgba(0, 123, 255, 0.9)",
                    borderWidth: "0",
                    backgroundColor: "rgba(0, 123, 255, 0.5)"
                            }
               
                        ]
        },
		
        options: {
            scales: {
                yAxes: [ {
                    ticks: {
                        //beginAtZero: true
						 min: 0,
            			 max: 100,
            			 stepSize: 20
						        
						        //steps: 100,
                                //stepValue: 100,
                                //max: 40 
                    }
                                } ]
            }
        }
    } );
	
	

 //bar chart
    var ctx3 = document.getElementById( "barChart3" );
    //    ctx.height = 200;
    var myChart3 = new Chart( ctx3, {
        type: 'bar',
        data: {
            labels: [ "Final Score"],
            datasets: [
                {
					
                    label: "organization Score",
                    data: [ 500 ],
                    borderColor: "rgba(0, 123, 255, 0.9)",
                    borderWidth: "0",
                    backgroundColor: "rgba(0, 123, 255, 0.5)"
                            }
                        ]
        },
        options: {
            scales: {
                yAxes: [ {
                    ticks: {
                         min: 0,
            			 max: 1000,
            			 stepSize: 100
                    }
                                } ]
            }
        }
    } );
	
	 
	


} )( jQuery );