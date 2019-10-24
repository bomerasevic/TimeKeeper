import React, { Component } from "react";
import logo from "./logo.svg";
import "./App.css";
import Home from "./components/Home/Home";
import AboutUs from "./components/AboutUs/AboutUs";
import services from "./data/services.json";
import Team from "./components/Team/Team";

class App extends React.Component {
    render() {
        return (
            <div className="App">
                <Home />
                <AboutUs />
                <Team />
            </div>
        );
    }
}

export default App;
