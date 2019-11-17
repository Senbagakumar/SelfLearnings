$(document).ready(function () {
    displayUpdateNewsViews();
});
function displayUpdateNewsViews() {
 
    $.ajax(
        {
            type: "POST", //HTTP POST Method 
            url: "/Home/updateNews", // Controller/View 

            success: function (result) {
            
              
                for (var r = 0; r < result.length; r++) {
                    console.log(result[r]);
                    $("#stl-update-news").append("<p>" + result[r] + "</p>");
                }
            },

        });
}