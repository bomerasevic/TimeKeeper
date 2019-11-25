import React from 'react';
import "./UserCard.css";
import { withStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import { fontSize } from '@material-ui/system';

const styles = {
  card: {
    width: 200,
    minHeight: 215,
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    position: 'relative',
    justifyContent: 'center',
    margin: '2.1rem'
  },  
  name: {
    marginTop: '-3rem'
  },
  pos: {
    textAlign: 'center',
    fontSize: '1rem'
  },
  img: {
      height: '80%',
      width: '100%',
      objectFit: 'contain',
      padding: '2rem'
  }
};

function SimpleCard(props) {
  const { classes } = props;
  

  return (
    <Card className={classes.card}>
      <CardContent>
        <img src="https://www.trzcacak.rs/myfile/detail/21-218752_icon-man-png-transparent-professional-icon-png.png" alt="" className={classes.img} />
        <Typography variant="h5" component="h2" className={classes.pos} >
          {props.name}
        </Typography>
        
      </CardContent>
     
    </Card>
  );
}


export default withStyles(styles)(SimpleCard);