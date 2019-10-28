import React, { Component } from "react";
import "./MobileNavigation.css";
import "../Navigation/Navigation.css";
class MobileNavigation extends Component {
    componentDidMount() {
        const M = window.M;
        document.addEventListener('DOMContentLoaded', function () {
        let elems = document.querySelectorAll('.sidenav');
        let instances = M.Sidenav.init(elems, {});
        });
        }

  render() {
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
}



export default MobileNavigation;
