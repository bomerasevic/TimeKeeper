import React from "react";
import "./MembersLogin.css";
import MembersView from "../MembersView/MembersView";
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import NavigationLogin from "../NavigationLogin/NavigationLogin";
import teams from "./../../data/team.json";
function MembersLogin() {
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
    const membersView = teams.map((member,i) => (
        <MembersView key={i} name={member.name}  />
    ));
  

    return (
      <div>
      
           
            <div className="row">
                <div className="container">
            
            <h1 className="teams">Team</h1>
         
                    
                    </div>
            </div>
           
            <div className="row">
            <Slider {...settings}>{membersView}</Slider>
            
            </div>
            
           
          </div>
            

       
        
    );
}
export default MembersLogin;