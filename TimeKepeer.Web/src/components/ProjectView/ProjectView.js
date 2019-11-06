import React, { Component } from "react";
import NavigationLogin from "../NavigationLogin/NavigationLogin";
import logo from "../../assets/images/puzzle.png";
import "./ProjectView.css";
import ProjectsList from "../ProjectsList/ProjectList";

function ProjectView() {
    return (
        <div>
            <NavigationLogin />
            <div class="table-project">
                <ProjectsList />
            </div>
        </div>
    );
}
export default ProjectView;
