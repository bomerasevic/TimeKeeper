//  import React, { Component } from 'react';
//  import NavigationLogin from '../NavigationLogin/NavigationLogin';
//  import { ScheduleComponent, Day, Week, Inject, ViewsDirective, ViewDirective } from '@syncfusion/ej2-react-schedule';
//  import { extend } from '@syncfusion/ej2-base';
//  import M from "materialize-css";



//  class Profile extends React.Component {

//  	constructor() {
//          super(...arguments);
//          this.data = extend([],  null, true);
//      }
//      render() {
//          return (
// 			<div>
//  		<NavigationLogin />
//  		<ScheduleComponent width='60%' height='550px' selectedDate={new Date()} eventSettings={{ dataSource: this.data }}>
//        <ViewsDirective>
//          <ViewDirective option='Day' interval={2} displayName='2 Days' startHour='08:30' endHour='17:00' timeScale={{ enable: true, slotCount: 5 }}/>
//          <ViewDirective option='Week' interval={2} displayName='2 Weeks' showWeekend={false} isSelected={true}/>
//        </ViewsDirective>
//        <Inject services={[Day, Week]}/>
//  		</ScheduleComponent>
//  		</div> );

//      }}
//     }



//  export default Profile;