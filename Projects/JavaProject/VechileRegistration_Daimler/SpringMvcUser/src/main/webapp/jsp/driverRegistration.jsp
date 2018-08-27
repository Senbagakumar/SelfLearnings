<%@ taglib uri = "http://java.sun.com/jsp/jstl/core" prefix = "c" %>

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
	<link href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600" rel="stylesheet">-->
    <!-- Required Fremwork -->
    <link rel="stylesheet" type="text/css" href="resources/bower_components/bootstrap/css/bootstrap.min.css">
    <!-- themify icon -->
    <link rel="stylesheet" type="text/css" href="resources/assets/icon/themify-icons/themify-icons.css">
    <!-- Font Awesome -->
    <link rel="stylesheet" type="text/css" href="resources/assets/icon/font-awesome/css/font-awesome.min.css">
     <!-- ico font -->
    <link rel="stylesheet" type="text/css" href="resources/assets/icon/icofont/css/icofont.css">
     <!-- Data Table Css -->
    <link rel="stylesheet" type="text/css" href="resources/bower_components/datatables.net-bs4/css/dataTables.bootstrap4.min.css">
    <link rel="stylesheet" type="text/css" href="resources/assets/pages/data-table/css/buttons.dataTables.min.css">
    <link rel="stylesheet" type="text/css" href="resources/bower_components/datatables.net-responsive-bs4/css/responsive.bootstrap4.min.css">
    <link rel="stylesheet" type="text/css" href="resources/assets/pages/data-table/extensions/responsive/css/responsive.dataTables.css">
    <!-- Date-time picker css -->
    <link rel="stylesheet" type="text/css" href="resources/assets/pages/advance-elements/css/bootstrap-datetimepicker.css">
    <!-- scrollbar.css -->
    <link rel="stylesheet" type="text/css" href="resources/assets/css/jquery.mCustomScrollbar.css">
    <!-- radial chart.css -->
    <link rel="stylesheet" href="resources/assets/pages/chart/radial/css/radial.css" type="text/css" media="all">
    <!-- Style.css -->
    <link rel="stylesheet" type="text/css" href="resources/assets/css/style.css">
    <script src="https://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script>
    function assignEdit(name,license_No,contact_No,license_validity,pass_type,pass_validity){
		
		document.getElementById("txtDriverName").value=name;
		document.getElementById("txtContactno").value=license_No;
		document.getElementById("txtLicenceNo").value=contact_No;
		document.getElementById("txtLicenceValitity").value=new Date(license_validity);
		document.getElementById("cmbPassType").value=pass_type;
		
		if(pass_type != "ONE"){
			document.getElementById("txtNTimePassValitity").value=new Date(pass_validity);
			document.getElementById("disDivAddEditForm").style.display="block";
			document.getElementById("lblNTimePass").style.display="block";
			document.getElementById("txtNTimePassValitity").style.display="block";
		}
		
		
	}
    $( document ).ready(function() {
    	
    	$("#linkAdd").click(function () {
    		$('#hdnAction').attr("value", "INSERT");
    		$('#submitBtn').text("Save");
    	});
    	
    	$("#btnUpdate").on('click',function(){
    		$('#hdnAction').attr("value", "UPDATE");
    		$('#submitBtn').text("Update");
    	});
    });
    </script>
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
                                <li class=" ">
                                    <a href="index.html">
                                        <span class="pcoded-micon"><i class="ti-home"></i><b>D</b></span>
                                        <span class="pcoded-mtext">Dashboard</span>
                                        <span class="pcoded-mcaret"></span>
                                    </a>
                                    
                                </li>
                                
                                <li class="pcoded-hasmenu ">
                                    <a href="javascript:void(0)">
                                        <span class="pcoded-micon"><i class="fa fa-check-square"></i><b>P</b></span>
                                        <span class="pcoded-mtext">Yard</span>                                        
                                        <span class="pcoded-mcaret"></span>
                                    </a>
                                    <ul class="pcoded-submenu">
										
                                        <li class="">
                                            <a href="yardIn.html">
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
                                
                                <li class="pcoded-hasmenu active">
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
                                        <li class="active ">
                                            <a href="javascript:void(0)">
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
                                <div class="page-wrapper" style="padding: 0px !important;">
                                    <!-- Page-body start -->
                                    <div class="page-body">
                                                                                
                                        <div class="card borderless-card">
                                            <div class="card-block default-breadcrumb">
                                                <div class="breadcrumb-header">
                                                    <h5><i class="icofont icofont-steering"></i> DRIVER REGISTRATION</h5>
                                                </div>
                                                <div class="m-t-10">
                                                    <ul class="breadcrumb-title">
                                                        <li class="breadcrumb-item">
                                                            <a href="index.html">
                                                                <i class="icofont icofont-home"></i>
                                                            </a>
                                                        </li>
                                                        <li class="breadcrumb-item"><a href="#!">VRO</a>
                                                        </li>
                                                        <li class="breadcrumb-item"><a href="#!">Driver Registration</a>
                                                        </li>
                                                       </ul>
                                                </div>
                                            </div>
                                        </div>
                                        
                                        <div class="card"  style="display:none" id="disDivAddEditForm">
                                            <div class="panel panel-default">
                                                <div class="panel-heading bg-default txt-white">
                                                   <i class="icofont icofont-plus-circle"></i> ADD DETAILS - DRIVER REGISTRATION
                                                   <span  class="float-right"  ><a id="linkClose" href="#"><i class="icofont icofont-ui-close"></i> Close</a></span> 
                                                </div>
                                                <div class="panel-body">
                                                   <form id="updateDriverFrm" action="saveDriver" method="POST" novalidate>
                                                    <div class="card-block">
                                                        <input type="hidden" id="hdnAction" name="hdnAction" value="INSERT"/>
                                                            <div class="form-group row">
                                                                <label class="col-sm-2 col-form-label">Driver Name</label>
                                                                <div class="col-sm-4">
                                                                    <input type="text" class="form-control" id="txtDriverName" name="txtDriverName" placeholder="Enter Driver Name">
                                                                    <span class="messages popover-valid"></span>
                                                                </div>
                                                                
                                                                <label class="col-sm-2 col-form-label">Contact No</label>
                                                                <div class="col-sm-4">
                                                                    <input type="mobile" maxlength="12" class="form-control" id="txtContactno" name="txtContactno" placeholder="Enter Contact No">
                                                                    <span class="messages popover-valid"></span>
                                                                </div>
                                                                
                                                            </div>
                                                            <div class="form-group row">
                                                                <label class="col-sm-2 col-form-label">Driving License No</label>
                                                                <div class="col-sm-4">
                                                                    <input type="text" class="form-control" id="txtLicenceNo" name="txtLicenceNo" placeholder="Enter Driving Licence No">
                                                                    <span class="messages popover-valid"></span>
                                                                </div>
                                                                
                                                               
                                                                <label class="col-sm-2 col-form-label">License Validity </label>
                                                                <div class="col-sm-4">
                                                                	<input class="form-control" type="date" id="txtLicenceValitity" name="txtLicenceValitity" placeholder="Select Driving Licence Valitity">    
                                                                   
                                                                    <span class="messages popover-valid"></span>
                                                                </div>                                                                
                                                                
                                                            </div>
                                                            <div class="form-group row">
                                                                <label class="col-sm-2 col-form-label">Pass Type</label>
                                                                <div class="col-sm-4">
                                                                    <select class="form-control" id="cmbPassType" name="cmbPassType" placeholder="Select Pass Type">
                                                                    <option value="" >-Select-</option>
                                                                    <option value="ONE" >One Day Pass</option>
                                                                    <option value="MULTI" >'N Time Pass</option>
                                                                    </select>
                                                                    <span class="messages popover-valid"></span>
                                                                </div>
                                                                
                                                              
                                                                <label id="lblNTimePass" class="col-sm-2 col-form-label">N' Time Pass Validity </label>
                                                                <div class="col-sm-4">
                                                                	<input class="form-control" type="date" id="txtNTimePassValitity" name="txtNTimePassValitity" placeholder="Select N'  Time Pass Valitity">    
                                                                   
                                                                    <span class="messages popover-valid"></span>
                                                                </div>
                                                                                                                              
                                                                
                                                            </div>
                                                            
                                                        
                                                    </div>
                                                   <div class="panel-footer">
                                                            <div class="row">
                                                                <!-- <label class="col-sm-2"></label> -->
                                                                <div class="col-sm-12" style="text-align: center;">
                                                                 <!--<button type="submit"  class="btn btn-default btn-primary">Daily Pass</button>&nbsp;
                                                                 <button type="submit"  class="btn btn-default btn-primary">Monthly Pass</button>&nbsp;-->
                                                                 <button type="submit" id="submitBtn" class="btn btn-default btn-primary"><i class="icofont icofont-save"></i> Save</button>&nbsp;
                                                                 <button type="reset"  class="btn btn-default btn-primary"><i class="icofont icofont-close-circled"></i>Cancel</button>
                                                                   
                                                                </div>
                                                            </div>
                                                   </div>
                                                   </form>
                                                </div>
                                            </div>
                                           
                                        </div>
                                           
                                            <div class="card">
                                            <div class="panel panel-default">
                                                <div class="panel-heading bg-default txt-white">
                                                   <i class="icofont icofont-list"></i> DRIVER LIST
                                                   <span  class="float-right"  ><a id="linkAdd" href="#"><i class="icofont icofont-plus"></i> ADD</a></span> 
                                                </div>
                                                <div class="panel-body">
                                                   <div class="card">
                                                     <div class="card-block">
                                                <div class="">
                                                    <div class="dt-responsive table-responsive ">
                                                    
                                                        <table id="new-cons" class="table  table-striped table-bordered nowrap">
                                                            <thead>
                                                                <tr>
                                                                    <th>#</th>                                                         
                                                                    <th>Driver Name</th>
                                                                    <th>Contact No</th>
                                                                    <th>License No</th>
                                                                    <th>License Validity</th>
                                                                    <th>Pass  Type</th>
                                                                    <th  data-hide="all">Monthly Pass Expiry Date</th>
                                                                    <th>Print</th>
                                                                    
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                            <c:forEach var="driver" items="${drivers}">
                                                            	<tr>
                                                                   
                                                                    <td>${driver.id}</td>
                                                                    <td>${driver.name}</td>
                                                                    <td>${driver.contact_No}</td>
                                                                    <td>${driver.license_No}</td>
                                                                    <td>${driver.license_Validity}</td>
                                                                    <td>${driver.pass_Type}</td>
                                                                    <td>${driver.pass_Expiry_Date}</td>
                                                                    
                                                                    <td>
                                                                    <button id="btnUpdate" class="btn btn-warning btn-icon" onclick="assignEdit('${driver.id}','${driver.name}','${driver.contact_No}','${driver.license_No}','${driver.license_Validity}','${driver.pass_Type}','${driver.pass_Expiry_Date}');"><i style="margin-right:0px;" class="icofont icofont-pencil"></i></button>&nbsp;
                                                                    <button class="btn btn-info btn-icon"><i style="margin-right:0px;"  class="icofont icofont-eye-alt"></i></button> &nbsp;
                                                                    <button class="btn btn-danger btn-icon"><i style="margin-right:0px;"  class="icofont icofont-delete-alt"></i></button>&nbsp;
                                                                    <button class="btn btn-primary btn-icon"><i style="margin-right:0px;"  data-toggle="modal" data-target="#print-Modal"  class="icofont icofont-print"></i></button></td>
                                                                </tr>
                                                            </c:forEach>
                                                                                                                                
                                                               
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                                   </div>
                                        <!-- Config. table end -->

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
    <script  src="resources/bower_components/jquery/js/jquery.min.js"></script>
    <script  src="resources/bower_components/jquery-ui/js/jquery-ui.min.js"></script>
    <script  src="resources/bower_components/popper.js/js/popper.min.js"></script>
    <script  src="resources/bower_components/bootstrap/js/bootstrap.min.js"></script>
    <!-- jquery slimscroll js -->
    <script  src="resources/bower_components/jquery-slimscroll/js/jquery.slimscroll.js"></script>
    <!-- modernizr js -->
    <script  src="resources/bower_components/modernizr/js/modernizr.js"></script>
    <script  src="resources/bower_components/modernizr/js/css-scrollbars.js"></script>
    <!-- Validation js -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/underscore.js/1.8.3/underscore-min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.10.6/moment.min.js"></script>
    <script  src="resources/assets/pages/form-validation/validate.js"></script>
    
    
    <!-- Bootstrap date-time-picker js -->
    <script  src="resources/assets/pages/advance-elements/moment-with-locales.min.js"></script>
    <script  src="resources/bower_components/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script  src="resources/assets/pages/advance-elements/bootstrap-datetimepicker.min.js"></script>
     <!-- data-table js -->
    <script src="resources/bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script src="resources/bower_components/datatables.net-buttons/js/dataTables.buttons.min.js"></script>
    <script src="resources/assets/pages/data-table/js/jszip.min.js"></script>
    <script src="resources/assets/pages/data-table/js/pdfmake.min.js"></script>
    <script src="resources/assets/pages/data-table/js/vfs_fonts.js"></script>
    <script src="resources/assets/pages/data-table/extensions/responsive/js/dataTables.responsive.min.js"></script>
    <script src="resources/bower_components/datatables.net-buttons/js/buttons.print.min.js"></script>
    <script src="resources/bower_components/datatables.net-buttons/js/buttons.html5.min.js"></script>
    <script src="resources/bower_components/datatables.net-bs4/js/dataTables.bootstrap4.min.js"></script>
    <script src="resources/bower_components/datatables.net-responsive/js/dataTables.responsive.min.js"></script>
    <script src="resources/bower_components/datatables.net-responsive-bs4/js/responsive.bootstrap4.min.js"></script>
    
    <!-- Custom js -->
    <script src="resources/assets/pages/data-table/extensions/responsive/js/responsive-custom.js"></script>
    <script  src="resources/assets/pages/form-validation/form-validation.js"></script>
     <!-- menu js -->
    <script src="resources/assets/js/pcoded.min.js"></script>
    <script src="resources/assets/js/navbar-image/vertical-layout.min.js "></script>
    
    <script src="resources/assets/js/jquery.mCustomScrollbar.concat.min.js"></script>
    <script  src="resources/assets/js/script.js"></script>
    <script type="application/javascript" >
	
	$(document).ready(function(){
		hideFunction();	
		$("#cmbPassType").on('change',function(){
		   if($(this).val()=="MULTI"){
			   $("#txtNTimePassValitity").show();
			   $("#lblNTimePass").show();		
		   }else{
			   hideFunction();
		   }
									  
		});
		
		function hideFunction(){
		     $("#txtNTimePassValitity").hide();
			 $("#lblNTimePass").hide();
		}
		$("#linkAdd").on('click',function(){
			 $("#disDivAddEditForm").show();
		});
		
		$("#linkClose").on('click',function(){
			 $("#disDivAddEditForm").hide();
		});
		
		$("#btnUpdate").on('click',function(){
			 $("#disDivAddEditForm").show();
		});
							   
	});
	
	</script>
</body>

</html>
