import React from "react";
import "./TeamsLogin.css";
import TeamsView from "../TeamsView/TeamsView";
import Members from "../Members/Members";
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import teams from "./../../data/team.json";
import NavigationLogin from "../NavigationLogin/NavigationLogin";
function Teams() {
    let settings = {
        dots: true,
        infinite: true,
        speed: 400,
        slidesToShow: 3,
        slidesToScroll: 1,

        responsive: [
            {
                breakpoint: 1200,
                settings: {
                    infinite: true,
                    slidesToShow: 3,
                    slidesToScroll: 1,
                    dots: true,
                    arrows: true
                }
            },
            {
                breakpoint: 980,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1,
                    dots: true,
                    arrows: true
                }
            },
            {
                breakpoint: 600,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1,
                    arrows: true
                }
            }
        ]
    };
    const teamView = teams.map((name,i) => (
        <TeamsView key={i} name={name}  />
    ));
    const teamMembers = teams.map((name,i) => (
        <Members key={i} name={name}  />
    ));
    return (
      <div>
      
            <NavigationLogin />
            <div className="row">
                <div className="container">
            
            <h1 className="teams">Teams</h1>
            <a className="btn btn-teams">
                        Add Teams
                    </a>
                    
                    </div>
            </div>
           
            <div className="row">
            <Slider {...settings}>{teamView}</Slider>
            
            </div>
            
           
          </div>
            

       
        
    );
}
export default Teams;
