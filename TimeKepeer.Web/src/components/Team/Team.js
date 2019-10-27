import React from "react";
import "./Team.css";
import Slider from "react-slick";
import TeamMember from "./../TeamMember/TeamMember";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import team from "./../../data/teamMembers.json";
function Team() {
    let settings = {
        dots: true,
        infinite: true,
        speed: 500,
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
                breakpoint: 1000,
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
                    slidesToScroll: 1
                }
            }
        ]
    };
    const teamMembers = team.map((member, i) => (
        <TeamMember key={i} name={member.name} role={member.role} icon={member.icon} />
    ));
    return (
        <div className="team large-section" id="team">
            <Slider {...settings}>{teamMembers}</Slider>
        </div>
    );
}
export default Team;
