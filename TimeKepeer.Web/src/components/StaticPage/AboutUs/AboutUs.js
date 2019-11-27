import React, { Component } from "react";
import "./AboutUs.css";
import stopwatch from "../../../assets/images/stopwatch.svg";
function AboutUs() {
	return (
		<div className="container white-section" id="about">
			<div className="row about-us-row">
				<h2 className="about-us-header">About us</h2>
			</div>
			<img className="stopwatch-icon" src={stopwatch} />
			<div className="about-us-text">
				<h6 className="about-us">
					The most impressive websites and app experiences are rooted in smart design, embody clear vision,
					and are backed by the right technology. Best-in-class digital projects require technology that
					perfectly supports the design and functionality. We rely on the right tools for the job, not a
					one-size-fits-all tech stack. We have refined user activity and also melded our Web development work
					with best practices. This makes our development programs the most competitive applications across
					the Globe.
				</h6>
			</div>
		</div>
	);
}
export default AboutUs;