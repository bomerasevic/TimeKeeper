import React, { Component } from "react";
import "./MobileNavigation.css";
import "../Navigation/Navigation.css";
function MobileNavigation() {
      
         const M = window.M;
         document.addEventListener('DOMContentLoaded', function () {
         let elems = document.querySelectorAll('.sidenav');
 		let instances = M.Sidenav.init(elems, {});
		
 		});

        
		
 	return (
		
 		<ul className="sidenav" id="mobile-demo">
 			<li>
 				<a href="#about">About us</a>
 			</li>
 			<li>
 				<a href="#services">Services</a>
 			</li>
			<li>
				<a href="#staff">Our staff</a>
 			</li>
			<li>
 				<a href="#contact">Contact us</a>
 			</li>
 			<li>
 				<a className="waves-effect waves-light btn">Login</a>
		</li>
		</ul>
 	);
 }

// class MobileNavigation extends Component {
// 	componentDidMount() {
// 	  const options = {
		  
// 		inDuration: 250,
// 		outDuration: 200,
// 		draggable: true
// 	  };
// 	  M.Sidenav.init(this.Sidenav);
  
// 	  let instance = M.Sidenav.getInstance(this.Sidenav);
// 	  instance.open();
// 	  console.log(instance.isOpen);
// 	}
// 	render() {
// 	  return (
// 		<div>
// 		  <ul
// 			ref={Sidenav => {
// 			  this.Sidenav = Sidenav;
// 			}}
// 			id="slide-out"
// 			className="sidenav"
// 		  >
// 			<li>
// 				<a href="#about">About us</a>
// 		</li>
// 			<li>
// 				<a href="#services">Services</a>
// 			</li>
//  			<li>
//  				<a href="#staff">Our staff</a>
//  			</li>
// 		<li>
//  				<a href="#contact">Contact us</a>
// 				  			</li>
// 			<li>
//  				<a className="waves-effect waves-light btn">Login</a>
// 		</li>
// 		  </ul>
		  
// 		</div>
// 	  );
// 	}
//   }
  

export default MobileNavigation;
