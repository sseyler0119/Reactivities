import { Link } from "react-router-dom"
import { Segment, Header, Icon, Button } from "semantic-ui-react"

const NotFound = () => {
  return (
    <Segment placeholder>
        <Header icon>
            <Icon name='search' />
            Oops, we've looked everywhere but could not find what you are looking for!
        </Header>
        <Segment.Inline>
            <Button as={Link} to='/activities'>Return to activities page</Button>
        </Segment.Inline>
    </Segment>
  )
}
export default NotFound