import React from 'react';
import {
  FlatList,
  StyleSheet,
} from 'react-native';
import Constants from 'expo-constants';
import { Item } from './Item';

import theme from '../assets/Theme';

export default function List (props) {
 
  const { data, openItem } = props;
  console.log("Dataaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa: ",data[0])
  return (
    <FlatList
      data={data}
      
      renderItem={({ item }) => (
        <Item
          style={styles.item}
          item={item}
          openItem={openItem}
          bottomDivider
          chevron

        />
      )}
      keyExtractor={item => item.id+""}
    //extraData={selected}
    />
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    marginTop: Constants.statusBarHeight,
    backgroundColor: theme.COLORS.LISTCOLOR,
  },
  item: {
    backgroundColor: theme.COLORS.LISTCOLOR,
    padding: 20,
    marginVertical: 8,
    marginHorizontal: 16,
    display: 'flex',
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: 3,
    height: 100
  },
  title: {
    fontSize: 28,
    position: "absolute",
    top: 0,
    left: 100,
    color: 'black'
  },
  description: {
    fontSize: 16,
    color: 'black',
    position: "absolute",
    top: 60,
    left: 100,
  },
  image: {
    width: 50,
    height: 50
  },



});