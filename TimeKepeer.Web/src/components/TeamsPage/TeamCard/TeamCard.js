import React from 'react';
import './TeamCard.css';
import '../TeamDescription/TeamDescription';
import { withStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';

const styles = {
  card: {
    width: 310,
    height: 340,
    display: 'block',
    flexDirection: 'column',
    alignItems: 'center',
    position: 'relative',
    justifyContent: 'center',
    margin: 'auto'
  },  
  name: {
    textAlign: 'center'
  },
  pos: {
    position: 'absolute',
    bottom: '.5rem'
  },
  description: {
    textAlign: 'justify'
  }
};

class  SimpleCard extends React.Component {
state={
  selectedTeam: null
}



  render(){
  const { classes, click, id, name, description } = this.props;
  

  return (
    <Card className={classes.card}>
      <CardContent>
        
        <Typography variant="h5" component="h2" className={classes.name}>
          {name}
        </Typography>
       

  <p className={classes.description}> {description} </p>
      </CardContent>
      <CardActions className={classes.pos}>
        <Button className ="btn learnMore" size="medium" >Learn More</Button>
      </CardActions>
    </Card>
  );
}}


export default withStyles(styles)(SimpleCard);
