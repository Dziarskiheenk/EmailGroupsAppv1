import React, { Fragment, useState } from 'react';
import { ListGroupItem, Collapse, Card, CardBody, Row, Col, Button } from "reactstrap";
import Question from './Question';
import MailGroupEdit from './MailGroupEdit';

export default function MailGroup(props) {
    const { mailGroup, isOpen, onClick, onDeleteGroup, onGroupEdit } = props;

    const [showDeleteQuestion, setShowDeleteQuestion] = useState(false);
    const [showGroupEditModal, setShowGroupEditModal] = useState(false);
    const toggleGroupEditModal = () => {
        setShowGroupEditModal(!showGroupEditModal);
    }

    return (
        <Fragment>
            <ListGroupItem tag='button' action onClick={onClick}>{mailGroup.name}</ListGroupItem>
            <Collapse isOpen={isOpen}>
                <Card>
                    <CardBody>
                        <Row>
                            <Col sm={12} className='text-center'>
                                <Button size='sm'>Add e-mail</Button>{' '}
                                <Button size='sm' onClick={toggleGroupEditModal}>Group details</Button>{' '}
                                <Button size='sm' onClick={() => { setShowDeleteQuestion(true) }}>Remove group</Button>
                            </Col>
                        </Row>
                        {mailGroup.description}
                    </CardBody>
                </Card>
            </Collapse>

            <Question
                showModal={showDeleteQuestion}
                content={'Are you sure you want to delete that group?'}
                noClicked={() => { setShowDeleteQuestion(false) }}
                yesClicked={() => {
                    onDeleteGroup(mailGroup.name);
                    setShowDeleteQuestion(false)
                }} />

            <MailGroupEdit
                showModal={showGroupEditModal}
                editedGroup={mailGroup}
                toggleModal={toggleGroupEditModal}
                onGroupEdit={onGroupEdit} />
        </Fragment>
    )
}