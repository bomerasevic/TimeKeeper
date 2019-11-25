import React from "react";
import "./TeamMember.css";
import facebook from "../../../assets/images/facebook.svg";
import linkedin from "../../../assets/images/linkedin.svg";
import github from "../../../assets/images/github.svg";
class TeamMember extends React.Component {
	render() {
		console.log(this.props.icon);
		return (
			<div className="rectangle">
				<img className="gray" src={this.props.icon} alt="description" />
				<h4>{this.props.name}</h4>
				<h6>{this.props.role}</h6>

				<div className="row">
					<div className="col s4 icon">
						<a href={this.props.gitLink}>
							<img className="grayIcon" src={github} />
						</a>
					</div>
					<div className="col s4 icon">
						<a href={this.props.fbLink}>
							<img className="grayIcon" src={facebook} />
						</a>
					</div>
					<div className="col s4 icon">
						<a href={this.props.linkedInLink}>
							<img className="grayIcon" src={linkedin} />
						</a>
					</div>
				</div>
			</div>
		);
	}
}
export default TeamMember;
