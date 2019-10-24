import React, { Component } from "react";
import "./AboutUs.css";
import stopwatch from "../../assets/images/stopwatch.svg";
function AboutUs() {
    return (
        <div className="container white-section" id="about">
            <div className="row about-us-row">
                <h2 className="about-us-header">About us</h2>
            </div>
            <img className="stopwatch-icon" src={stopwatch} />
            <div className="about-us-text">
                <h6 className="about-us">
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor
                    incididunt ut labore et dolore magna aliqua. Lorem ipsum dolor sit amet,
                    consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et
                    dolore magna aliqua. Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                    sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                </h6>
            </div>
        </div>
    );
}
export default AboutUs;
