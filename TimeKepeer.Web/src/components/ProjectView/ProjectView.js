import React, { Component } from "react";
import NavigationLogin from "../NavigationLogin/NavigationLogin";
import logo from "../../assets/images/puzzle.png";
import "./ProjectView.css";
import ProjectsList from "../ProjectsList/ProjectList";

function ProjectView() {
    return (
        <div>
            <NavigationLogin />
            <div className="row">
                <h3 className="table-name">Projects</h3>
                <a className=" btn modal-trigger add-btn">Add Project</a>
            </div>
            <div class="table-projects">
                <ProjectsList />
            </div>
        </div>
    );
}
export default ProjectView;
