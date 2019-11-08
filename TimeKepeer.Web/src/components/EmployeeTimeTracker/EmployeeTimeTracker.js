import React, { Component } from 'react';
import NavigationLogin from '../NavigationLogin/NavigationLogin';
import { DatePicker, RangePicker, theme } from 'react-trip-date';
import {ThemeProvider} from 'styled-components';
import { withRouter } from "react-router-dom";



const  handleResponsive  =  setNumberOfMonth  =>  {
	let  width  =  document.querySelector('.tp-calendar').clientWidth;
	if  (width  >  900)  {
		setNumberOfMonth(1);
	}  else  if  (width  <  900  &&  width  >  580)  {
		setNumberOfMonth(1);
	}  else  if  (width  <  580)  {
		setNumberOfMonth(1);
	}
};

const  Day = ({  day  }) => {
	return  (
		<>
			<p  className="date">{day.format('DD')}</p>
		
		</>
		);
	};
	
class Profile extends React.Component {
    

  onChange = date => console.log(date)

  render() {
    return (
       
      <ThemeProvider theme={theme}>
          <NavigationLogin />
           <div className="boxCalendar">
        <DatePicker
          //handleChange={onChange}
		  selectedDays={['2019-11-06']} //initial selected days
		  jalali={false}
		  numberOfMonths={3}
		  numberOfSelectableDays={3} // number of days you need 
		
		  responsive={handleResponsive} // custom responsive, when using it, `numberOfMonths` props not working
		  disabledBeforToday={true} 
		  disabled={false} // disable calendar 
		  dayComponent={Day} //custom day component 
		  //titleComponent={Title} // custom title of days
        />
        </div>
      </ThemeProvider>
    );
  }
}



export default Profile;