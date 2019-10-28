import React from "react";
import "./TeamMember.css";
import facebook from "../../assets/images/facebook.svg";
import linkedin from "../../assets/images/linkedin.svg";
import github from "../../assets/images/github.svg";
class TeamMember extends React.Component {
    render() {
        console.log(this.props.icon);
        return (
            <div className="rectangle">
                <img className="gray" src={this.props.icon} alt="description"></img>
                <h4>{this.props.name}</h4>
                <h6>{this.props.role}</h6>

                <div className="row">
                    <div className="col s4 icon">
                        <img className="grayIcon" src={github} />
                    </div>
                    <div className="col s4 icon">
                        <img className="grayIcon" src={facebook} />
                    </div>
                    <div className="col s4 icon">
                        <img className="grayIcon" src={linkedin} />
                    </div>
                </div>
            </div>
        );
    }
}
export default TeamMember;
