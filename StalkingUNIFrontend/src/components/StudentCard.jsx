import Box from '@mui/material/Box';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';


export default function StudentCard({ matricola, name, surname }) {
  return (
    <Box sx={{ minWidth: '26ch' }}>
      <Card variant="outlined">
        <CardContent>
          <Typography variant="h5" component="div">{matricola}</Typography>

          <Typography sx={{ color: 'text.secondary' }}>Nome: {name}</Typography>
          <Typography sx={{ color: 'text.secondary' }}>Cognome: {surname}</Typography>
        </CardContent>
        <CardActions>
          <Button size="small" href={`/user/${matricola}`}>Apri profilo</Button>
        </CardActions>
      </Card>
    </Box>
  );
}