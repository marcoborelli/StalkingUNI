import { useState } from 'react';
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';

import StudentCard from '../components/StudentCard'
import Searchbar from '../components/Searchbar'


export default function ResponsiveGrid() {
  const [student, setStudents] = useState([])

  return (
    <Box sx={{ flexGrow: 1 }} >
      <Grid container>
        <Grid size={12} sx={{ position: 'sticky', top: 10, backgroundColor: 'white', zIndex: 10 }}>
          <Searchbar updateSearchResult={setStudents} />
        </Grid>
        <Grid container size={12} sx={{ paddingTop: '3ch' }} spacing={{ xs: 2, sm: 2, md: 2, lg: 3, xl: 4 }}>
          {Array.isArray(student) && /*TODO: implementare il rederict diretto nel caso la ricerca abbia prodotto un solo studente*/
            student.map((elem, index) => (
              <Grid key={index} size={{ xs: 12, sm: 6, md: 4, lg: 3, xl: 2.4 }} sx={{ display: 'flex', justifyContent: 'center' }}>
                <StudentCard
                  name={elem.Nome}
                  surname={elem.Cognome}
                  matricola={elem.Matricola}
                />
              </Grid>
            ))}
        </Grid>
      </Grid>
    </Box>
  );
}
