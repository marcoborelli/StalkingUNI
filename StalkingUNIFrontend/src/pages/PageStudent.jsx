import { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom'
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';

import Button from '@mui/material/Button';
import { Link } from 'react-router-dom'

import TableEsami from '../components/TableEsami'
import api from '../utilities/api'


export default function BasicGrid() {
  const [rows, setRows] = useState([])
  const [userData, setUserData] = useState({})
  const { matricola } = useParams()

  useEffect(() => {
      const fetchData = async () => {
        try {
          const response = await api.get(`/getVoti?matricola=${matricola}`)
          setRows(response.data)
        } catch (error) {
          console.error('Error fetching user data:', error)
        }

        try {
          const response = await api.get(`/getStudent?matricola=${matricola}`)
          console.log(response.data)
          
          if (!response.data || Array.isArray(response.data)) {
            console.log("C'è un bel problemuccio")
          }

          setUserData(response.data)
        } catch (error) {
          console.error('Error fetching user data:', error)
        }
      }

      fetchData()
    }, [])


  return (
    <Box sx={{ flexGrow: 1 }}>
      <Link to={`/search`}>
            <Button size="small">Torna indietro</Button>
          </Link>
      <Grid container spacing={2}>
        <Grid size={12}>
          <Typography variant="h6" component="div"> {userData.Nome} {userData.Cognome} {`MAT. ${userData.Matricola}`}</Typography>
        </Grid>
        <Grid size={12}>
          <TableEsami data={rows}/>
        </Grid>
      </Grid>
    </Box>
  );
}
