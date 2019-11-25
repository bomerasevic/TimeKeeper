import React from "react";
import { connect } from "react-redux";
import { loadEmployees } from "../../store/actions";

class PostList extends React.Component {
  componentDidMount() {
    this.props.loadEmployees();
  }

  renderList() {
    console.log(this.props, 'component')
    return this.props.employees.map(employee => {
      return (
        <div className="item" key={employee.id}>
          <i className="large middle aligned icon user" />
          <div className="content">
            <div className="description">   
              <h2>{employee.firstName}</h2>
              <p>{employee.email}</p>
            </div>
          </div>
        </div>
      );
    });
  }

  render() {
    
    console.log(this.props.employees);
    return (
    <div className="ui relaxed divided list">{this.renderList()}</div>
    );
  }
}

const mapStateToProps = state => {
  return {
    employees: state.employees
  };
};
// first argument null if no state
export default connect(mapStateToProps, { loadEmployees })(PostList);
