<!DOCTYPE html>
<html lang="en">

<head>
    <title>DAIMLER </title>
    
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta name="author" content="codedthemes" />
    <!-- Favicon icon -->
    <link rel="icon" href="resources/assets/images/favicon.ico" type="image/x-icon">
    <!-- Google font
	<link href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600" rel="stylesheet"> -->
    <!-- Required Fremwork -->
    <link rel="stylesheet" type="text/css" href="resources/bower_components/bootstrap/css/bootstrap.min.css">
    <!-- themify icon -->
    <link rel="stylesheet" type="text/css" href="resources/assets/icon/themify-icons/themify-icons.css">
    <!-- Font Awesome -->
    <link rel="stylesheet" type="text/css" href="resources/assets/icon/font-awesome/css/font-awesome.min.css">
     <!-- ico font -->
    <link rel="stylesheet" type="text/css" href="resources/assets/icon/icofont/css/icofont.css">

    <!-- scrollbar.css -->
    <link rel="stylesheet" type="text/css" href="resources/assets/css/jquery.mCustomScrollbar.css">
    <!-- radial chart.css -->
    <link rel="stylesheet" href="resources/assets/pages/chart/radial/css/radial.css" type="text/css" media="all">
    <!-- Style.css -->
    <link rel="stylesheet" type="text/css" href="resources/assets/css/style.css">
    <style>
     .bg-c-gray{
		 background:linear-gradient(45deg, #C7C7C7, #9c9696);
	 }
	
	</style>
</head>

<body>
    <!-- Pre-loader start -->
    <div class="theme-loader">
        <div class="loader-track">
            <div class="loader-bar"></div>
        </div>
    </div>
    <!-- Pre-loader end -->
    <div id="pcoded" class="pcoded">
        <div class="pcoded-overlay-box"></div>
        <div class="pcoded-container navbar-wrapper">
            <nav class="navbar header-navbar pcoded-header">
                <div class="navbar-wrapper">
                    <div class="navbar-logo">
                        <a class="mobile-menu" id="mobile-collapse" href="#!">
                            <i class="ti-menu"></i>
                        </a>
                        <div class="mobile-search">
                            <div class="header-search">
                                <div class="main-search morphsearch-search">
                                    <div class="input-group">
                                        <span class="input-group-addon search-close"><i class="ti-close"></i></span>
                                        <input type="text" class="form-control" placeholder="Enter Keyword">
                                        <span class="input-group-addon search-btn"><i class="ti-search"></i></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <a href="index.html">
                            <img class="img-fluid" src="resources/assets/images/logo.png" alt="Theme-Logo" />
                        </a>
                        <a class="mobile-options">
                            <i class="ti-more"></i>
                        </a>
                    </div>

                    <div class="navbar-container container-fluid">
                        <ul class="nav-left">
                            <li>
                                <div class="sidebar_toggle"><a href="javascript:void(0)"><i class="ti-menu"></i></a></div>
                            </li>
                            <li class="header-search">
                                <div class="main-search morphsearch-search">
                                    <div class="input-group">
                                        <span class="input-group-addon search-close"><i class="ti-close"></i></span>
                                        <input type="text" class="form-control">
                                        <span class="input-group-addon search-btn"><i class="ti-search"></i></span>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <a href="#!" onClick="javascript:toggleFullScreen()">
                                    <i class="ti-fullscreen"></i>
                                </a>
                            </li>
                        </ul>
                        <ul class="nav-right">
                            <li class="header-notification">
                                <a href="#!">
                                    <i class="ti-bell"></i>
                                    <span class="badge bg-c-pink"></span>
                                </a>
                                <ul class="show-notification">
                                    <li>
                                        <h6>Notifications</h6>
                                        <label class="label label-danger">New</label>
                                    </li>
                                    <li>
                                        <div class="media">
                                            <img class="d-flex align-self-center img-radius" src="resources/assets/images/avatar-2.jpg" alt="Generic placeholder image">
                                            <div class="media-body">
                                                <h5 class="notification-user">Truck Name</h5>
                                                <p class="notification-msg">Lorem ipsum dolor sit amet, consectetuer elit.</p>
                                                <span class="notification-time">30 minutes ago</span>
                                            </div>
                                        </div>
                                    </li>
                                    
                                </ul>
                            </li>
                           
                            <li class="user-profile header-notification">
                                <a href="#!">
                                    <img src="resources/assets/images/avatar-4.jpg" class="img-radius" alt="User-Profile-Image">
                                    <span>John Doe</span>
                                    <i class="ti-angle-down"></i>
                                </a>
                                <ul class="show-notification profile-notification">
                                    <li>
                                        <a href="#!">
                                            <i class="ti-settings"></i> Settings
                                        </a>
                                    </li>
                                    <li>
                                        <a href="#">
                                            <i class="ti-user"></i> Profile
                                        </a>
                                    </li>
                                    <li>
                                        <a href="#">
                                            <i class="ti-email"></i> My Messages
                                        </a>
                                    </li>
                                    <li>
                                        <a href="#">
                                            <i class="ti-lock"></i> Lock Screen
                                        </a>
                                    </li>
                                    <li>
                                        <a href="#">
                                        <i class="ti-layout-sidebar-left"></i> Logout
                                    </a>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
            
            <!-- Sidebar inner chat end-->
            <div class="pcoded-main-container">
                <div class="pcoded-wrapper">
                    <nav class="pcoded-navbar">
                        <div class="sidebar_toggle"><a href="#"><i class="icon-close icons"></i></a></div>
                        <div class="pcoded-inner-navbar main-menu">

                            <ul class="pcoded-item pcoded-left-item">
                                <li class=" active ">
                                    <a href="javascript:void(0)">
                                        <span class="pcoded-micon"><i class="ti-home"></i><b>D</b></span>
                                        <span class="pcoded-mtext">Dashboard</span>
                                        <span class="pcoded-mcaret"></span>
                                    </a>
                                    
                                </li>
                                
                                <li class="pcoded-hasmenu">
                                    <a href="javascript:void(0)">
                                        <span class="pcoded-micon"><i class="fa fa-check-square"></i><b>P</b></span>
                                        <span class="pcoded-mtext">Yard</span>                                        
                                        <span class="pcoded-mcaret"></span>
                                    </a>
                                    <ul class="pcoded-submenu">
										
                                        <li class=" ">
                                            <a href="yardIn">
                                                <span class="pcoded-micon"><i class="ti-layout"></i></span>
                                                <span class="pcoded-mtext">Yard In</span>
                                                <span class="pcoded-mcaret"></span>
                                            </a>
                                        </li>
                                        <li class=" ">
                                            <a href="slots.html">
                                                <span class="pcoded-micon"><i class="ti-layout"></i></span>
                                                <span class="pcoded-mtext">Yard Slots</span>
                                                <span class="pcoded-mcaret"></span>
                                            </a>
                                        </li>
                                        <li class="">
                                            <a href="vehicleList.html">
                                                <span class="pcoded-micon"><i class="ti-angle-right"></i></span>
                                                <span class="pcoded-mtext">Truck List</span>
                                                <span class="pcoded-mcaret"></span>
                                            </a>
                                        </li>
                                        
                                    </ul>
                                </li>
                                
                                <li class="pcoded-hasmenu">
                                    <a href="javascript:void(0)">
                                        <span class="pcoded-micon"><i class="icofont icofont-long-drive"></i><b>W</b></span>
                                        <span class="pcoded-mtext">VRO</span>                                        
                                        <span class="pcoded-mcaret"></span>
                                    </a>
                                    <ul class="pcoded-submenu">
                                        <li class="">
                                            <a href="vehicleRegistration.html">
                                                <span class="pcoded-micon"><i class="ti-angle-right"></i></span>
                                                <span class="pcoded-mtext">Vehicle Registration</span>
                                                <span class="pcoded-mcaret"></span>
                                            </a>
                                        </li>
                                        <li class=" ">
                                            <a href="driverRegistration.html">
                                                <span class="pcoded-micon"><i class="ti-angle-right"></i></span>
                                                <span class="pcoded-mtext">Driver Registration</span>
                                                <span class="pcoded-mcaret"></span>
                                            </a>
                                        </li>
                                       
                                    </ul>
                                </li>
                                <!-- <i class="fa fa-truck"></i> -->
                                
                                <li class="pcoded-hasmenu">
                                    <a href="javascript:void(0)">
                                        <span class="pcoded-micon"><i class="fa fa-truck"></i><b>W</b></span>
                                        <span class="pcoded-mtext">Loading</span>
                                        <span class="pcoded-mcaret"></span>
                                    </a>
                                    <ul class="pcoded-submenu">
                                        <li class=" ">
                                            <a href="unloadingDockEntry.html">
                                                <span class="pcoded-micon"><i class="ti-angle-right"></i></span>
                                                <span class="pcoded-mtext">Unloading Dock Entry</span>
                                                <span class="pcoded-mcaret"></span>
                                            </a>
                                        </li>
                                        <li class=" ">
                                            <a href="emptyDockEntry.html">
                                                <span class="pcoded-micon"><i class="ti-angle-right"></i></span>
                                                <span class="pcoded-mtext">Empty Dock Entry</span>
                                                <span class="pcoded-mcaret"></span>
                                            </a>
                                        </li>
                                     </ul>
                                 </li>     
                                        
                                <li class="pcoded-hasmenu">
                                    <a href="javascript:void(0)">
                                        <span class="pcoded-micon"><i class="icofont icofont-upload"></i><b>W</b></span>
                                        <span class="pcoded-mtext">IBL</span>                                        
                                        <span class="pcoded-mcaret"></span>
                                    </a>
                                    <ul class="pcoded-submenu">
                                        <li class="">
                                            <a href="uploadCriticalPart.html">
                                                <span class="pcoded-micon"><i class="ti-angle-right"></i></span>
                                                <span class="pcoded-mtext">Upload Critical Part</span>
                                                <span class="pcoded-mcaret"></span>
                                            </a>
                                        </li>
                                        <li class=" ">
                                            <a href="approveCriticalPart.html">
                                                <span class="pcoded-micon"><i class="ti-angle-right"></i></span>
                                                <span class="pcoded-mtext">Approve Critical Part</span>
                                                <span class="pcoded-mcaret"></span>
                                            </a>
                                        </li>
                                        
                                    </ul>
                                </li>
                                
                                <li class="">
                                    <a href="gateEntry.html">
                                        <span class="pcoded-micon"><i class="icofont icofont-ui-unlock"></i><b>N</b></span>
                                        <span class="pcoded-mtext">Gate Entry</span>
                                        <span class="pcoded-mcaret"></span>
                                    </a>
                                </li>
                                <li class="">
                                    <a href="vehicleTracking.html">
                                        <span class="pcoded-micon"><i class="fa fa-map-marker"></i><b>N</b></span>
                                        <span class="pcoded-mtext">Truck Tracker</span>
                                        <span class="pcoded-mcaret"></span>
                                    </a>
                                </li>
                                
                            </ul>
                            
                        </div>
                    </nav>
                    <div class="pcoded-content">
                        <div class="pcoded-inner-content">
                            <!-- Main-body start -->
                            <div class="main-body">
                                <div class="" style="padding: :0px !important;">
                                    <!-- Page-body start -->
                                    <div class="page-body">
                                        <div class="row">
                                            <!-- order-card start -->
                                            <div class="col-md-6 col-xl-3">
                                                <div class="card bg-c-blue order-card">
                                                    <div class="card-block">
                                                        <h6 class="m-b-20  text-light font-weight">Critical Parts Received</h6>
                                                        <h2><img src="resources/assets/images/brakes_02.png" style="margin-top: -13px;"><span  class="f-right font-style"> 15</span></h2>
                                                        <p class="m-b-0">Critical Parts For Today<span class="f-right">40</span></p>
                                                        <!--<p class="m-b-0">Completed Orders<span class="f-right">351</span></p>-->
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-xl-3">
                                                <div class="card bg-c-green order-card">
                                                    <div class="card-block">
                                                        <h6 class="m-b-20 text-light font-weight">Trucks in Yard</h6>
                                                        <h2 class="font-style"><img src="resources/assets/images/parking_01.png"><span class="f-right"> 39</span></h2>
                                                        <p class="m-b-0">Total Slots<span class="f-right">45</span></p>
                                                       <!-- <p class="m-b-0">This Month<span class="f-right">213</span></p>-->
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-xl-3">
                                                <div class="card bg-c-yellow order-card">
                                                    <div class="card-block">
                                                        <h6 class="m-b-20 text-light font-weight">Trucks in Service Lane</h6>
                                                        <h2 class="font-style"><img src="resources/assets/images/road-with-broken-line_02.png" ><span class="f-right">30</span></h2>
                                                        <p class="m-b-0">Average Waiting Time<span class="f-right">25 Mins</span></p>                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-xl-3">
                                                <div class="card bg-c-pink order-card">
                                                    <div class="card-block">
                                                        <h6 class="m-b-20 text-light font-weight">Processed Trucks</h6>
                                                        <h2 class="font-style"><i class="fa fa-truck"></i><span class="f-right">30</span></h2>
                                                        <p class="m-b-0">Completed Orders<span class="f-right">351</span></p>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- order-card end -->

                                            <!-- statustic and process start -->
                                            <div class="col-lg-8 col-md-12">
                                                <div class="card">
                                                    <div class="card-header ">
                                                        <h5 class=" text-dark">Truck Processing Trend</h5>
                                                        
                                                        <!--<div class="card-header-right">
                                                            <ul class="list-unstyled card-option">
                                                                <li><i class="fa fa-chevron-left"></i></li>
                                                                <li><i class="fa fa-window-maximize full-card"></i></li>
                                                                <li><i class="fa fa-minus minimize-card"></i></li>
                                                                <li><i class="fa fa-refresh reload-card"></i></li>
                                                                <li><i class="fa fa-times close-card"></i></li>
                                                            </ul>
                                                        </div>-->
                                                    </div>
                                                    <div class="card-block">
                                                        <div id="Statistics-chart" style="height:200px"></div>
                                                       
                                                        
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-12">
                                                <div class="card">
                                                    <div class="card-header">
                                                        <h5 class="text-dark">Critical Parts Status</h5>
                                                    </div>
                                                    <div class="card-block">                                                        
                                                        <canvas id="feedback-chart" height="100"></canvas>
                                                        <div class="row justify-content-center" style="margin-top:27px;">
                                                            <div class="col-auto b-r-default m-t-5 m-b-5">
                                                                <h4>83%</h4>
                                                                <p class="text-custom m-b-0"><i class="fa fa-square"></i> Transit</p>
                                                            </div>
                                                            <div class="col-auto m-t-5 m-b-5">
                                                                <h4>17%</h4>
                                                                <p class="text-danger m-b-0"><i class="fa fa-square"></i> Received</p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- statustic and process end -->
                                            <div class="col-md-6 col-xl-3">
                                                <div class="card seo-card">
                                                    <div class="card-block seo-statustic">
                                                        <h6 class="m-b-20 "><i class="ti-server text-c-green"></i> Trucks Processed Today</h6>
                                                        <h5 class="text-dark">200</h5>
                                                        
                                                    </div>
                                                    <div class="seo-chart">
                                                        <canvas id="seo-card1" style="height: 110px !important;"></canvas>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-xl-3">
                                                <div class="card seo-card">
                                                    <div class="card-block seo-statustic">
                                                        <h6 class="m-b-20"><i class="ti-reload text-c-blue"></i> Trucks Processed MTD</h6>
                                                        <h5 class="text-dark">300</h5>
                                                        
                                                    </div>
                                                    <div class="seo-chart">
                                                        <canvas id="seo-card2" style="height: 110px !important;"></canvas>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                           <!-- <div class="col-md-6 col-xl-3">
                                                <div class="card bg-c-yellow notification-card">
													<div class="card-block">
														<div class="row align-items-center">
															<div class="col-4 notify-icon"><i class="fa fa-truck"></i></div>
															<div class="col-8 notify-cont">
																<h4>200</h4>
																<p>&nbsp;&nbsp; Trucks Processed Today</p>
															</div>
														</div>
													</div>
												</div>
                                            </div>
                                            <div class="col-md-6 col-xl-3">
                                                <div class="card bg-c-pink notification-card">
													<div class="card-block">
														<div class="row align-items-center">
															<div class="col-4 notify-icon"><i class="fa fa-truck"></i></div>
															<div class="col-8 notify-cont">
																<h4>300</h4>
																<p> &nbsp;&nbsp; Trucks Processed MTD </p>
															</div>
														</div>
													</div>
												</div>
                                            </div>-->
                                            <div class="col-md-6 col-xl-3">
                                                <div class="card bg-c-yellow notification-card">
													<div class="card-block">
														<div class="row align-items-center" style="height: 150px;">
															<div class="col-4 notify-icon"><i class="fa fa-truck"></i></div>
															<div class="col-8 notify-cont">
                                                                <p class="text-dark"><h6 calss="m-b-20 " style="color:#fff"> Avg Trucks Parked in service lane per day </h6></p>
																<h5>12</h5>
																
                                                                
															</div>
														</div>
													</div>
												</div>
                                            </div>
    
                                            <div class="col-md-6 col-xl-3">
                                              <div class="card bg-c-pink notification-card">
													<div class="card-block">
														<div class="row align-items-center" style="height: 150px;">
															<div class="col-4 notify-icon"><i class="fa fa-truck"></i></div>
															<div class="col-8 notify-cont">
                                                                 <p class="text-dark"><h6 calss="m-b-20 " style="color:#fff"> Avg Trucks Parked in service lane per day </h6></p>
																 <h5> 1.30 Mins</h5>
																                  
															</div>
														</div>
													</div>
												</div>
                                                
                                            </div>
                                            
                                            
                                        </div>
                                    </div>
                                    <!-- Page-body end -->
                                </div>
                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Required Jquery -->
    <script  src="resources/bower_components/jquery/js/jquery.min.js "></script>
    <script  src="resources/bower_components/jquery-ui/js/jquery-ui.min.js "></script>
    <script  src="resources/bower_components/popper.js/js/popper.min.js"></script>
    <script  src="resources/bower_components/bootstrap/js/bootstrap.min.js "></script>
    <script  src="resources/assets/pages/widget/excanvas.js "></script>
    <!-- jquery slimscroll js -->
    <script  src="resources/bower_components/jquery-slimscroll/js/jquery.slimscroll.js "></script>
    <!-- modernizr js -->
    <script  src="resources/bower_components/modernizr/js/modernizr.js "></script>
    <!-- slimscroll js -->
    <script  src="resources/assets/js/SmoothScroll.js"></script>
    <script src="resources/assets/js/jquery.mCustomScrollbar.concat.min.js "></script>
    <!-- Chart js -->
    <script  src="resources/bower_components/chart.js/js/Chart.js"></script>
    <script src="resources/assets/pages/widget/amchart/amcharts.js"></script>
    <script src="resources/assets/pages/widget/amchart/serial.js"></script>
    <script src="resources/assets/pages/widget/amchart/light.js"></script>
    <!-- menu js -->
    <script src="resources/assets/js/pcoded.min.js"></script>
    <script src="resources/assets/js/navbar-image/vertical-layout.min.js "></script>
    <!-- Morris Chart js 
    <script src="resources/bower_components/raphael/js/raphael.min.js"></script>
    <script src="resources/bower_components/morris.js/js/morris.js"></script>-->
    <!-- custom js -->
    <!--<script  src="resources/assets/pages/dashboard/custom-dashboard.min.js"></script>-->
    <script  src="resources/assets/pages/dashboard/custom-dashboard.js"></script>
    <script  src="resources/assets/js/script.js "></script>
</body>

</html>
