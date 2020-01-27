import React, { Fragment, useState } from 'react';
import { ListGroupItem, Collapse, Card, CardBody, Row, Col, Button } from "reactstrap";
import Question from './Question';
import MailGroupEdit from './MailGroupEdit';
import PropTypes from "prop-types";
import MailAddressEdit from './MailAddressEdit';

export default function MailGroup(props) {
    const { mailGroup, isOpen, onClick, onDeleteGroup, onGroupEdit } = props;

    const [showDeleteQuestion, setShowDeleteQuestion] = useState(false);
    const [showGroupEditModal, setShowGroupEditModal] = useState(false);
    const toggleGroupEditModal = () => {
        setShowGroupEditModal(!showGroupEditModal);
    }

    const [showMailAddressEditModal, setShowMailAddressEditModal] = useState(false);

    return (
        <Fragment>
            <ListGroupItem tag='button' action onClick={onClick}>{mailGroup.name}</ListGroupItem>
            <Collapse isOpen={isOpen}>
                <Card>
                    <CardBody>
                        <Row>
                            <Col sm={12} className='text-center'>
                                <Button size='sm' onClick={() => { setShowMailAddressEditModal(!showMailAddressEditModal) }}>Add e-mail</Button>{' '}
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

            <MailAddressEdit showModal={showMailAddressEditModal} />

        </Fragment>
    )
}

MailGroup.propTypes = {
    mailGroup: PropTypes.object.isRequired,
    isOpen: PropTypes.bool,
    onClick: PropTypes.func.isRequired,
    onDeleteGroup: PropTypes.func.isRequired,
    onGroupEdit: PropTypes.func.isRequired
}